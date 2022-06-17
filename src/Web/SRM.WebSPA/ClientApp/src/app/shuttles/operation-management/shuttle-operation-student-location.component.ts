import { Component, ViewChild, ElementRef, NgZone } from "@angular/core";
import { MessageService } from "prime/message/messageservice";
import { BasePageComponent } from "shared/components";
import {
	ShuttleService,
	ExcelService,
	StudentService,
	AdviceService
} from "shared/services";
import { Student } from "shared/models";
import { SelectItem } from "primeng/primeng";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
declare var google: any;

@Component({
	selector: "shuttle-operation-student-location",
	templateUrl: "./shuttle-operation-student-location.component.html"
})
export class ShuttleOperationLocationComponent extends BasePageComponent {
	rowData: any[];
	rowDataAdvice: any[];
	totalAdviceCount: number = 50;

	options: any;

	overlays: any[];

	dialogVisible: boolean;

	markerTitle: string;

	selectedPosition: any;

	infoWindow: any;

	bounds: any;

	draggable: boolean;

	shuttleOperationId: any;

	showAdvice: boolean = false;

	lastAdviceCount: number = 0;
	maxHeight: number = 100;
	studentScrollHeight: string = "400px";

	shuttle: any;
	mapStyle: any;
	leftStyle: any;
	displayStudentAvailableDialog: boolean = false;
	///gmap component variable taken from html
	@ViewChild("gmap") gmapInputEl: any;

	constructor(
		public service: ShuttleService,
		private route: ActivatedRoute,
		public studentService: StudentService,
		public messageService: MessageService,
		public excelService: ExcelService,
		public adviceService: AdviceService,
		public zone: NgZone
	) {
		super(messageService);
	}

	ngOnInit() {
		super.ngOnInit();

		this.options = {
			center: { lat: 40.9805, lng: 29.1121 },
			zoom: 12,
			disableDefaultUI: true,
			mapTypeControl: true,
			mapTypeControlOptions: {
				style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
				position: google.maps.ControlPosition.TOP_RIGHT

			},
			streetViewControl: true

		};

		this.infoWindow = new google.maps.InfoWindow();

		this.maxHeight = window.innerHeight - 130;// * 0.85;
		this.studentScrollHeight = (this.maxHeight * 0.50) + "px";
		//   this.gmapInputEl.style.height = this.maxHeight;
		this.mapStyle = { width: '100%', height: this.maxHeight - 50 - 62 + 'px' }
		this.leftStyle = { width: '100%', height: this.maxHeight - 62 + 'px' }
		//var mapEvents = new H.mapevents.MapEvents(this.gmapInputEl);
		if (!this.shuttleOperationId) {
			this.shuttleOperationId = Number(this.route.snapshot.paramMap.get('id'));

			this.showAdvice = false;
			this.rowDataAdvice = [];
			this.totalAdviceCount = 0;
			this.bounds = new google.maps.LatLngBounds();
			if (this.shuttleOperationId) {
				this.shuttleOperationId = this.shuttleOperationId;
				this.getShuttleDetail(this.shuttleOperationId);
				this.getShuttleLocations(this.shuttleOperationId);
			}
		}
		// Add event listener:
		//map.addEventListener('tap', function(evt) {
		// Log 'tap' and 'mouse' events:
		//console.log(evt.type, evt.currentPointer.type);
		//});

		this.service.shuttleOperationLocationLoad$.subscribe(
			(shuttleOperationId: any) => {
				this.showAdvice = false;
				this.rowDataAdvice = [];
				this.totalAdviceCount = 0;
				this.bounds = new google.maps.LatLngBounds();
				if (shuttleOperationId) {
					this.shuttleOperationId = shuttleOperationId;
					this.getShuttleLocations(shuttleOperationId);
				}
			}
		);
		this.service.studentAvailableTimeShow$.subscribe((studentId: any) => {
			this.displayStudentAvailableDialog = true;
			this.studentService.studentLoadedSource.next(studentId);
		});
		this.service.studentAdviceFinish$.subscribe((studentId: any) => {
			this.adviceButtonCreated=false;
			this.getShuttleLocations(this.shuttleOperationId);
		});
	}

	getShuttleDetail(shuttleOperationId) {
		this.overlays = [];
		let studentInfos = [];
		var requestObject = { id: shuttleOperationId };
		this.service
			.getStudentShuttleOperationById(requestObject)
			.subscribe(response => {
				if (response.isSuccess) {
					this.shuttle = response.resultValue;
					this.shuttle.statusMessage = this.shuttle.statusMessage != null ? this.shuttle.statusMessage : 'Servis Başlamadı';
				}
			});
	}
	getShuttleLocations(shuttleOperationId) {

		this.overlays = [];
		let studentInfos = [];
		var requestObject = { shuttleOperationId: shuttleOperationId };
		this.service
			.getShuttleOperationStudentLocations(requestObject)
			.subscribe(response => {
				if (response.isSuccess) {
					var hasLocation = false;
					this.rowData = response.resultValue;
					for (const row of this.rowData) {
						if (row.locations) {
							for (const location of row.locations) {
								if (
									location.locationX != null &&
									location.locationY != null
								) {
									var hasLocation = true;
									var studentInfo = {
										studentId: row.studentId,
										operationId: row.id,
										studentName: row.studentName,
										location: location,
										isCompensation: row.isCompensation,
										operationStatus: row.operationStatus
									};
									studentInfos.push(studentInfo);
								}
							}
						}
					}

					if (hasLocation == false) {
						this.messageService.add({
							severity: "info",
							summary: "Konum Bulunamadı",
							detail: "Servise ait konum bilgisi tespit edilemedi"
						});
						this.setMapCenter(40.9805, 29.1121, 10);
					} else {
						this.setStudentInfoOnMap(
							studentInfos,
							this.gmapInputEl.map
						);
					}
				}
			});
	}

	setStudentInfoOnMap(studentInfos, map) {
		//var infowindow = new google.maps.InfoWindow();

		for (let i = 0; i < studentInfos.length; i++) {
			if (
				(!studentInfos[i].location.locationX &&
					!studentInfos[i].location.locationY) ||
				(studentInfos[i].location.locationX == 0 &&
					studentInfos[i].location.locationY == 0)
			)
				continue;
			let url = "assets/layout/images/mapsicons/";
			//let url = "http://maps.google.com/mapfiles/ms/icons/";
			let color;

			var title = studentInfos[i].studentName;
			if (studentInfos[i].isCompensation) {
				title += '(Telafi)'
				color = "sari";
			} else {
				switch (studentInfos[i].operationStatus) {
					case 0:
						title += '(Beklemede)'
						color = "mor";
						break;
					case 1:
						title += '(Geldi)'
						color = "yesil";
						break;
					case 2:
						title += '(Gelmedi)'
						color = "kirmizi";
						break;
					default:
						break;
				}
			}

			url += color + ".png";

			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(
					parseFloat(studentInfos[i].location.locationX.replace(',', '.')),
					parseFloat(studentInfos[i].location.locationY.replace(',', '.'))
				),
				title: title,
				icon: {
					url: url,
					scaledSize: new google.maps.Size(40, 40)
				},
				operationInfo: {
					isAdvice: false,
					studentInfo: studentInfos[i]
				}
			});

			this.overlays.push(marker);
			//extend the bounds to include each marker's position
			this.bounds.extend(marker.position);
		}

		//now fit the map to the newly inclusive bounds
		map.fitBounds(this.bounds);
		if (!this.adviceButtonCreated && this.adviceButtonClicked) {
			this.generateAdviceButton(map);
		}
	}
	adviceButtonCreated: boolean = false;
	adviceButtonClicked: boolean = true;
	generateAdviceButton(map) {
		var controlDiv = document.createElement('div');
		// Set CSS for the control border
		var controlUI = document.createElement('div');
		controlUI.className = "mapAdviceButton";
		controlUI.title = 'Harita üzerinde önerileri gösterin';
		controlDiv.appendChild(controlUI);
		// Set CSS for the control interior
		var controlText = document.createElement('div');
		controlText.style.color = '#2d353c';
		controlText.style.fontFamily = 'Lato, Helvetica Neue, sans-serif';
		controlText.style.fontSize = '16px';
		controlText.style.lineHeight = '38px';
		controlText.style.paddingLeft = '5px';
		controlText.style.paddingRight = '5px';
		controlText.innerHTML = 'Önerileri Göster';
		controlUI.appendChild(controlText);
		this.adviceButtonClicked = false;
		this.adviceButtonCreated = true;
		var self = this;
		this.zone.runOutsideAngular(() => {
			this.zone.run(() => {
				controlUI.addEventListener('click', function () {
					self.getAdviceForMap();
					self.adviceButtonClicked = true;
					self.gmapInputEl.map.controls[google.maps.ControlPosition.LEFT_TOP].pop()
				});
			});

		});
		map.controls[google.maps.ControlPosition.LEFT_TOP].push(controlDiv);
	}

	async getAdviceForMe(event) {
		if (this.showAdvice) {
			// alert("fatafito");
			this.getAdviceForMap();
		}
	}

	removeAdvice() {
		//    var overlalays = this.overlays;       
		var removedAdviceCount = 0;
		if (this.rowDataAdvice) {
			removedAdviceCount = this.rowDataAdvice.length;
		}
		// var removedAdviceCount= this.rowDataAdvice.length;
		this.overlays.splice(this.overlays.length - removedAdviceCount, removedAdviceCount);
	}
	paginateAdvice(event) {

		let a = event;
		let pageNumber = event.page;
		this.getAdviceForMap(pageNumber);
	}

	getAdviceForMap(pageNumber = 0) {
		this.removeAdvice();
		this.showAdvice = true;
		let studentInfos = [];
		// this.overlays = [];

		var bounds = this.gmapInputEl.map.getBounds();
		var areaBounds = {
			north: bounds.getNorthEast().lat(),
			south: bounds.getSouthWest().lat(),
			east: bounds.getNorthEast().lng(),
			west: bounds.getSouthWest().lng()
		};

		var requestObject = {
			pageSize: 10,
			pageNumber: pageNumber,
			shuttleOperationId: this.shuttleOperationId,
			mapsCorners: [
				{
					locationX: areaBounds.south,
					locationY: areaBounds.west
				},
				{
					locationX: areaBounds.north,
					locationY: areaBounds.west
				},
				{
					locationX: areaBounds.north,
					locationY: areaBounds.east
				},
				{
					locationX: areaBounds.south,
					locationY: areaBounds.east
				}
			]
		};
		this.adviceService
			.getAdviceForMap(requestObject)
			.subscribe(response => {
				this.rowDataAdvice = [];
				this.totalAdviceCount = 0;
				if (response.isSuccess) {
					var hasAdvice = false;
					this.rowDataAdvice = response.resultValue.advices;
					this.totalAdviceCount = response.resultValue.totalCount;
					for (const row of this.rowDataAdvice) {
						hasAdvice = true;
						studentInfos.push({
							studentId: row.studentId,
							studentName: row.name + " " + row.surname,
							lat: row.locationX,
							lng: row.locationY,
							isAvailable: row.isAvaible,
							disContinuityCount: row.disContinuityCount,
							mounthlyDiscontinuityCount: row.mounthlyDiscontinuityCount
						});
					}

					if (hasAdvice == false) {
						this.messageService.add({
							severity: "info",
							summary: "Öneri Bulunamadı",
							detail: "Haritada seçili bölge için öneri bulunamadı"
						});
					} else {
						this.setStudentAdviceInfoOnMap(
							studentInfos,
							this.gmapInputEl.map
						);
					}
				}
			});
	}

	setStudentAdviceInfoOnMap(studentInfos, map) {
		for (let i = 0; i < studentInfos.length; i++) {
			if (
				(!studentInfos[i].lat && !studentInfos[i].lng) ||
				(studentInfos[i].lat == 0 && studentInfos[i].lng == 0)
			)
				continue;
			let url = "assets/layout/images/mapsicons/";
			let color;
			var title = studentInfos[i].studentName;
			if (studentInfos[i].isAvailable) {
				title += '(Öneri Müsait)'
				color = "mavi";
			} else {
				title += '(Öneri Müsait Değil)'
				color = "gri";
			}

			url += color + ".png";

			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(
					studentInfos[i].lat,
					studentInfos[i].lng
				),
				title: title,
				icon: {
					url: url,
					scaledSize: new google.maps.Size(40, 40)
				},
				operationInfo: {
					isAdvice: true,
					studentInfo: studentInfos[i]
				}
			});

			this.overlays.push(marker);
			//extend the bounds to include each marker's position
			this.bounds.extend(marker.position);
		}
		//(optional) restore the zoom level after the map is done scaling
		var listener = google.maps.event.addListener(map, "idle", function () {
			google.maps.event.removeListener(listener);
		});
	}

	showLocation(record) {
		if (record.locations) {
			this.showStudentLocation(record.studentName, record.locations[0].locationX, record.locations[0].locationY);
		} else {
			this.messageService.add({
				severity: "warn",
				summary: "Konum Bulunamadı",
				detail: record.studentName + " konum bilgisi girilmemiştir"
			});
		}
	}

	showLocationAdvice(record) {
		let locationX = record.locationX;
		let locationY = record.locationY;
		let nameSurname = record.name + " " + record.surname;
		this.showStudentLocation(nameSurname, locationX, locationY);

	}

	showStudentLocation(stundentName, locationX, locationY) {

		if ((!locationX && !locationY) || (locationX == 0 && locationY == 0)) {
			this.messageService.add({
				severity: "warn",
				summary: "Konum Bulunamadı",
				detail: stundentName + " konum bilgisi girilmemiştir"
			});
		} else {
			if ((typeof locationY === "string")) {
				locationX = parseFloat(locationX.replace(',', '.'));
				locationY = parseFloat(locationY.replace(',', '.'));
			}

			for (const overlay of this.gmapInputEl.overlays) {
				if (parseFloat(overlay.position.lat().toFixed(7)) == parseFloat(locationX.toFixed(7))
					&& parseFloat(overlay.position.lng().toFixed(7)) == parseFloat(locationY.toFixed(7))) {
					this.showPinLocation(this.gmapInputEl.map, overlay);
				}
			}

			this.gmapInputEl.map.setCenter(
				new google.maps.LatLng(
					locationX, locationY
				)
			);
		}
	}

	handleOverlayClick(event) {
		this.showPinLocation(event.map, event.overlay);
	}

	showPinLocation(map, overlay) {
		let isMarker = overlay.getTitle != undefined;

		if (isMarker) {
			let title = overlay.getTitle();
			let operationInfo = overlay.operationInfo;

			var span = "";
			if (operationInfo.isAdvice) {

				span = "<span  class='span-advice'>Öneri</span>"
			}


			this.infoWindow.setContent("<div>" + title + span + "</div>");
			this.infoWindow.open(map, overlay);
			//map.setCenter(overlay.getPosition());

			this.messageService.add({
				severity: "info",
				summary: "Konum Seçildi",
				detail: title
			});
		} else {
			this.messageService.add({
				severity: "info",
				summary: "Hata olustu",
				detail: ""
			});
		}
	}

	handleDragEnd(event) {
		this.messageService.add({
			severity: "info",
			summary: "Marker Dragged",
			detail: event.overlay.getTitle()
		});
	}

	handleMapClick(event) {
		this.dialogVisible = true;
		this.selectedPosition = event.latLng;
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

	onZoomChanged(event) {
		this.getAdviceForMe(event);
	}
	onMapDragEnd(event) {
		this.getAdviceForMe(event);
	}

	startStudenCall(advice) {
		var studenOperation = {
			operationId: this.shuttleOperationId,
			advice: advice
		}
		this.service.studentAdviceCallLoadedSource.next(studenOperation);
	}


	checkDate() {
		var newDate = new Date();
		var currentDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate())
		if (this.shuttle.dateTime) {
			return this.shuttle.dateTime < currentDate;
		}
		else {
			return false;
		}
	}
}
