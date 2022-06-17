import {
    Component,
    Input,
    ViewChild,
    ElementRef,
    Renderer
} from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MessageService } from "prime/message/messageservice";
import { StudentAddress, LocationRegion } from "shared/models";
import {
    StudentAddressService,
    StudentService,
    LocationRegionService
} from "shared/services";
import { ConfirmationService, SelectItem } from "primeng/primeng";
import { StudentBasePageComponent } from "shared/components/basepages/student-basepage.component";
declare var google: any;
import { LoaderService } from "shared/components/loader/loader.service";

@Component({
    selector: "student-address",
    templateUrl: "./student-address.component.html"
})
export class StudentAddressComponent extends StudentBasePageComponent<
    StudentAddress,
    StudentAddressService
> {
    public LocationRegions: SelectItem[];
    public selectedLocationRegion: number;
    public lastLocationRegion: number;
    options: any;
    overlays: any[];
    marker;
    newLocationStart = false;
    updateLocationStart = false;

    locationX: number;
    locationY: number;

    @ViewChild("gmap") gmapInputEl: any;

    constructor(
        public studentService: StudentService,
        public service: StudentAddressService,
        public locationRegionService: LocationRegionService,
        public messageService: MessageService,
        public confirmationService: ConfirmationService,
        public loaderService: LoaderService,
        private rd: Renderer
    ) {
        super(
            StudentAddress,
            studentService,
            service,
            messageService,
            confirmationService
        );
    }

    ngOnInit() {
        this.baseForm = new FormGroup({
            title: new FormControl("", Validators.required),
            addressInfo: new FormControl("", Validators.required),
            addressDirections: new FormControl(""),
            locationRegion: new FormControl(""),
            locationX: new FormControl(""),
            locationY: new FormControl("")
        });
        super.ngOnInit();
        this.record = new StudentAddress();

        this.LocationRegions = [{ label: "Seçiniz", value: null }];
        this.locationRegionService.getActiveRegions().subscribe(response => {
            var entities = <LocationRegion[]>response.resultValue;
            for (let entity of entities) {
                this.LocationRegions.push({
                    label: entity.name,
                    value: entity.id
                });
            }
        });
        this.setMapCenter(40.9805, 29.1121, 10);                   
 
        this.overlays = [];
    }

    loadData(studentId) {
        this.overlays = [];

        this.service.get(studentId).subscribe(response => {
            if (response.resultValue && response.resultValue.length > 0) {
                this.record = response.resultValue[0];
                this.newRecord = false;
                if (this.record.address.locationRegion) {
                    this.selectedLocationRegion = this.record.address.locationRegion.id;
                    this.lastLocationRegion = this.record.address.locationRegion.id;
                }

                if (this.record.address.location.latitude != 0) {
                    this.locationX = Number(
                        this.record.address.location.latitude
                    );
                    this.locationY = Number(
                        this.record.address.location.longitude
                    );
                    console.log(
                        "locationX:" +
                            this.locationX +
                            " locationY:" +
                            this.locationY
                    );
                    this.marker = new google.maps.Marker({
                        position: { lat: this.locationX, lng: this.locationY },
                        title: this.record.address.title,
                        draggable: false,
                        animation: google.maps.Animation.DROP
                    });
                    this.overlays.push(this.marker);                  
                    this.setMapCenter(this.locationX, this.locationY, 16);                   
                }
            } else {
                this.newRecord = true;
            }
        });
    }

    setMapCenter(locationX, locationY, zoom) {
        if (this.gmapInputEl.map) {
            this.gmapInputEl.map.setCenter(
                new google.maps.LatLng(locationX, locationY)
            );
            this.gmapInputEl.map.setZoom(zoom);
        } else {
            this.options = {
                center: { lat: locationX, lng: locationY },
                zoom: zoom
            };
        }
    }

    initData(rowData: StudentAddress[]) {
        for (let record of rowData) {
        }
        return rowData;
    }

    initAdd(record: StudentAddress) {}

    beforeSave(record: any) {}

    afterSave(record: any) {}

    initEdit(record: StudentAddress) {}

    save() {
        if (super.isValid(this.baseForm)) {
            this.beforeSave(this.baseForm.value);

            if (
                this.locationX != this.baseForm.value.locationX ||
                this.locationY != this.baseForm.value.locationY
            ) {
                this.baseForm.value.locationX = this.locationX;
                this.baseForm.value.locationY = this.locationY;
            }

            if (this.newRecord == false) {
                if (
                    this.lastLocationRegion !=
                    this.baseForm.value.locationRegion
                ) {
                    this.confirmationService.confirm({
                        message:
                            "Bölgeyi değiştiriyorsunuz öğrencinin bağlı olduğu taslaklar silinecektir, onaylıyor musunuz?",
                        header: "Onaylıyor musunuz?",
                        icon: "pi pi-question-circle",
                        accept: () => {
                            this.service
                                .put(
                                    this.studentId,
                                    this.record.id,
                                    this.baseForm.value
                                )
                                .subscribe(response => {
                                    if (response.isSuccess) {
                                        this.messageService.add({
                                            severity: "success",
                                            summary: "İşlem Başarılı",
                                            detail:
                                                "Kayıt Başarıyla Güncellendi."
                                        });
                                        this.messageService.add({
                                            severity: "warn",
                                            summary: "Uyarı",
                                            detail:
                                                "Bölgeyi değiştirdiğiniz zaman öğrencinin bağlı olduğu taslaklar kaybolacaktır. Sonrasında yeni taslak tanımı yapmanız gerekmketedir."
                                        });
                                        this.loadData(this.studentId);
                                        this.displayDialog = false;
                                    } else {
                                        this.showErrors(response);
                                    }
                                });
                        },
                        reject: () => {}
                    });
                } else {
                    this.service
                        .put(
                            this.studentId,
                            this.record.id,
                            this.baseForm.value
                        )
                        .subscribe(response => {
                            if (response.isSuccess) {
                                this.messageService.add({
                                    severity: "success",
                                    summary: "İşlem Başarılı",
                                    detail: "Kayıt Başarıyla Güncellendi."
                                });
                                this.loadData(this.studentId);
                                this.displayDialog = false;
                            } else {
                                this.showErrors(response);
                            }
                        });
                }
            } else {
                this.service
                    .post(this.studentId, this.baseForm.value)
                    .subscribe(response => {
                        if (response.isSuccess) {
                            this.messageService.add({
                                severity: "success",
                                summary: "İşlem Başarılı",
                                detail: "Kayıt Başarıyla Eklendi."
                            });
                            this.loadData(this.studentId);
                            this.displayDialog = false;
                        } else {
                            this.showErrors(response);
                        }
                    });
            }
        } else {
            this.messageService.add({
                severity: "warn",
                summary: "Uyarı",
                detail: "Formu doğru şekilde doldurunuz."
            });
        }
    }
    setMapLocation() {
        this.newLocationStart = true;
        var center = this.gmapInputEl.map.getCenter();
        this.marker = new google.maps.Marker({
            position: center,
            draggable: true,
            animation: google.maps.Animation.BOUNCE
        });
        this.overlays.push(this.marker);

        this.messageService.add({
            severity: "info",
            summary: "Konum Bilgisi",
            detail:
                "Konum bilgisi girişi için balonu sürükleyin ve balonun üzerine tıklayın ",
            life: 15000
        });
    }

    getAddressInfoFromMap(record) {
        var messageService = this.messageService;
        var loaderService = this.loaderService;
        loaderService.show();

        var pos = this.marker.getPosition();
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode(
            {
                latLng: pos
            },
            function(results, status) {
                loaderService.hide();
                if (status == google.maps.GeocoderStatus.OK) {
                    var address = results[0].formatted_address;
                    record.address.addressInfo = address;
                    messageService.add({
                        severity: "info",
                        summary: "Adres Bilgisi Güncellendi",
                        detail: address
                    });
                } else {
                    var errorStr =
                        "Cannot determine address at this location." + status;

                    messageService.add({
                        severity: "error",
                        summary: "Adres Bilgisi Alma Sırasında Hata Oluştu",
                        detail: errorStr
                    });
                }
            }
        );
    }

    toggleBounce() {
        if (this.marker.getAnimation() !== null) {
            this.marker.setAnimation(null);
            this.marker.setDraggable(false);
        } else {
            this.marker.setDraggable(true);
            this.marker.setAnimation(google.maps.Animation.BOUNCE);

            this.messageService.add({
                severity: "info",
                summary: "Konum Bilgisi",
                detail:
                    "Konum bilgisi girişi için balonu sürükleyin ve balonun üzerine tıklayın ",
                life: 15000
            });

            this.updateLocationStart = true;
        }
    }
    handleMapClick(event) {}

    handleOverlayClick(event) {
        if (this.marker.getAnimation() != null) {
            var position = event.overlay.getPosition().toJSON();
            if (this.newLocationStart) {
                this.newLocationStart = false;
            }
            this.record.address.location.latitude = position.lat;
            this.record.address.location.longitude = position.lng;

            this.locationX = position.lat;
            this.locationY = position.lng;

            this.marker.setAnimation(null);
            this.marker.setDraggable(false);

            this.updateLocationStart = false;
        }
    }

    handleDragEnd(event) {
        // this.messageService.add({
        //     severity: "info",
        //     summary: "Marker Dragged",
        //     detail: event.overlay.getTitle()
        // });
    }
}
