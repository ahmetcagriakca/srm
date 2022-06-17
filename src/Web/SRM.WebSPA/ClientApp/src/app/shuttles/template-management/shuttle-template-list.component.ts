import { Component } from "@angular/core";
import { MessageService } from "prime/message/messageservice";
import { BasePageComponent } from "shared/components";
import {
    ShuttleTemplateService,
    LocationRegionService,
    StudentServiceService,
    StudentService
} from "shared/services";
import { SelectItem, ConfirmationService } from "primeng/primeng";
import {
    LocationRegion,
    ShuttleTemplate,
    Student,
    ApplicationParameter
} from "shared/models";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ApplicationParameterService } from "shared/services/parameters/application-parameter.service";

@Component({
    selector: "shuttle-template-list",
    templateUrl: "./shuttle-template-list.component.html"
})
export class ShuttleTemplateListComponent extends BasePageComponent {
    displayShuttleDialog: boolean;
    id: number;
    rowData: any[];
    shuttleTemplateDay: number = 1;
    public LocationRegions: SelectItem[];
    public selectedLocationRegion: number[];
    public StudentServices: SelectItem[];
    public selectedStudentService: number;
    public DayOfWeekList: ApplicationParameter[];
    public DayOfWeeks: SelectItem[];
    public selectedDayOfWeek: number;
    CompletedLessonCounts: SelectItem[];

    constructor(
        public service: ShuttleTemplateService,
        public messageService: MessageService,
        private locationRegionService: LocationRegionService,
        private studentServiceService: StudentServiceService,
        private applicationParameterService: ApplicationParameterService,
        private studentService: StudentService,
        public confirmationService: ConfirmationService
    ) {
        super(messageService);
    }
    studentForm: FormGroup;
    ngOnInit() {
        this.record = new ShuttleTemplate();
        this.baseForm = new FormGroup({
            id: new FormControl(""),
            locationRegion: new FormControl("", Validators.required),
            studentService: new FormControl("", Validators.required),
            dayOfWeek: new FormControl("", Validators.required),
            time: new FormControl("", Validators.required),
            isActive: new FormControl("", Validators.required)
        });
        this.studentForm = new FormGroup({
            student: new FormControl("", Validators.required),
            order: new FormControl("", Validators.required),
            lessonCount: new FormControl("", Validators.required)
        });

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
        this.StudentServices = [{ label: "Seçiniz", value: null }];
        this.studentServiceService.get().subscribe(response => {
            var entities = <any[]>response.resultValue;
            for (let entity of entities) {
                this.StudentServices.push({
                    label: entity.plate,
                    value: entity.id
                });
            }
        });
        this.DayOfWeeks = [{ label: "Seçiniz", value: null }];
        this.applicationParameterService
            .getByName("DayOfWeek")
            .subscribe(response => {
                this.DayOfWeekList = <ApplicationParameter[]>(
                    response.resultValue
                );
                for (let entity of this.DayOfWeekList) {
                    this.DayOfWeeks.push({
                        label: entity.description,
                        value: entity.value
                    });
                }
                this.loadData();
            });
        this.CompletedLessonCounts = [{ label: "Seçiniz", value: null }];
        for (let index = 1; index < 3; index++) {
            var lessonCount = index.toString();
            this.CompletedLessonCounts.push({
                label: lessonCount,
                value: lessonCount
            });
        }
        this.recordStudent = {};
        super.ngOnInit();
    }

    setInfo(rowData: any[]) {
        var x = rowData;
        if (x && x.length > 0) {
            for (let row of x) {
                this.DayOfWeekList.forEach(element => {
                    if (row.dayOfWeek == element.value) {
                        row.dayOfWeekName = element.description;
                    }
                });
                if (row.time) {
                    row.time = new Date(row.time);
                }
            }
            this.rowData = x;
        } else {
            this.rowData = x;
        }
    }

    loadData() {
        this.getShuttleTemplates();
    }

    getShuttleTemplates() {
        // var requestObject = { day: this.shuttleTemplateDay }
        this.service
            .getShuttleTemplateByDayOfWeek(this.selectedDayOfWeek)
            .subscribe(response => {
                this.setInfo(response.resultValue);
            });
    }

    public displayDialog: boolean;
    public displayDeleteDialog: boolean;
    public newRecord: boolean;
	public deleteId: any;
	public templateIsActive:boolean;
    initAdd(record: any) {
        this.selectedLocationRegion = null;
        this.selectedStudentService = null;
        // return "Init Data not implemented";
    }

    initData(rowData: any[]) {
        // return "Init Data not implemented";
        this.selectedLocationRegion = null;
        this.selectedStudentService = null;
        return rowData;
    }

    initEdit(record: any) {
        this.selectedLocationRegion = record.locationRegion.id;
        this.selectedStudentService = record.studentService.id;
    }
    public record;
    showDialogToAdd() {
        this.newRecord = true;
        this.deleteId = 0;
        this.record = new ShuttleTemplate();
        // this.record = this.getNew();
        this.initAdd(this.record);
		this.displayDialog = true;
		this.templateIsActive=true;
    }
    showDialogToEdit(record) {
        this.newRecord = false;
        this.record = Object.assign({}, record);
        this.initEdit(this.record);
		this.displayDialog = true;
		this.templateIsActive=record.isActive;
    }
    // showDialogToDelete(record) {
    // 	this.deleteId = record.id;
    // 	this.displayDeleteDialog = true;
    // }
    okDelete(isDeleteConfirm: boolean) {
        if (isDeleteConfirm) {
            this.service.delete(this.deleteId).subscribe(response => {
                if (response.isSuccess) {
                    this.messageService.add({
                        severity: "success",
                        summary: "İşlem Başarılı",
                        detail: "Kayıt Başarıyla Silindi"
                    });
                    this.deleteId = 0;
                    this.loadData();
                } else {
                    this.showErrors(response);
                }
            });
        }
        this.displayDeleteDialog = false;
        this.displayDialog = false;
    }

    cancel() {
        this.record = new ShuttleTemplate();
        this.displayDialog = false;
    }

    beforeSave(record: any) {
        record.locationRegionId = this.selectedLocationRegion;
        record.studentServiceId = this.selectedStudentService;
    }

    save() {
        if (super.isValid()) {
            this.beforeSave(this.record);           
            if (this.newRecord == false) {
                this.service
                    .put(this.record.id, this.record)
                    .subscribe(response => {
                        if (response.isSuccess) {
                            this.messageService.add({
                                severity: "success",
                                summary: "İşlem Başarılı",
                                detail: "Kayıt Başarıyla Güncellendi."
                            });
                            this.loadData();
                        } else {
                            this.showErrors(response);
                        }
                    });
            } else {
                this.service.post(this.record).subscribe(response => {
                    if (response.isSuccess) {
                        this.messageService.add({
                            severity: "success",
                            summary: "İşlem Başarılı",
                            detail: "Kayıt Başarıyla Eklendi."
                        });
                        this.loadData();
                    } else {
                        this.showErrors(response);
                    }
                });
            }
            this.displayDialog = false;
        } else {
            this.messageService.add({
                severity: "warn",
                summary: "Uyarı",
                detail: "Formu doğru şekilde doldurunuz."
            });
        }
    }

    newStudentRecord: boolean = true;
    displayStudentDialog: boolean = false;
    recordStudent: any;
    selectedStudentId: any;
    deleteStudentTemplateId: any;
    deleteShuttleTemplateId: any;
    public Students: SelectItem[];
    getLocationStudents(locationId, selectedStudentId?) {
        this.Students = [];
        if (locationId) {
            this.studentService
                //.getStudentByLocationId(locationId)
                .getStudents()
                .subscribe(response => {
                    var entities = <Student[]>response.resultValue;
                    for (let entity of entities) {
                        this.Students.push({
                            label: entity.name + " " + entity.surname,
                            value: entity.id
                        });
                    }
                    this.selectedStudentId = selectedStudentId;
                });
        }
    }
    showTemplateStudentDialogToAdd(shuttle) {
        this.recordStudent = {};
        this.recordStudent.shuttleTemplateId = shuttle.id;
        this.displayStudentDialog = true;
        this.newStudentRecord = true;
        this.getLocationStudents(shuttle.locationRegion.id, null);
    }
    showTemplateStudentDialogToEdit(shuttle, record) {
        this.newStudentRecord = false;
        this.recordStudent = Object.assign({}, record);
        this.displayStudentDialog = true;
        this.getLocationStudents(
            shuttle.locationRegion.id,
            this.recordStudent.studentId
        );
    }

    cancelStudent() {
        this.recordStudent = {};
        this.displayStudentDialog = false;
        this.selectedStudentId = null;
    }

    showStudentDialogToDelete(record: any) {
        this.deleteStudentTemplateId = record.shuttleStudentTemplateId;
        this.confirmationService.confirm({
            message:
                record.name +
                " isimli öğrenciyi servis taslağından silmek istediğinize emin misiniz?",
            header: "Onaylıyor musunuz?",
            icon: "pi pi-question-circle",
            accept: () => {
                this.service
                    .DeleteStudentTemplate(this.deleteStudentTemplateId)
                    .subscribe(response => {
                        if (response.isSuccess) {
                            this.messageService.add({
                                severity: "success",
                                summary: "İşlem Başarılı",
                                detail: "Kayıt Başarıyla Silindi"
                            });
                            this.deleteStudentTemplateId = 0;
                            this.loadData();
                        } else {
                            this.showErrors(response);
                        }
                    });
            },
            reject: () => {}
        });
        this.displayStudentDialog = false;
    }

    saveStudent() {
        if (super.isValid(this.studentForm)) {
            if (this.newStudentRecord == false) {
                var studentTemplateUpdateRequest = {
                    studentTemplateId: this.recordStudent
                        .shuttleStudentTemplateId,
                    order: this.recordStudent.order,
                    lessonCount: this.recordStudent.lessonCount
                };
                this.service
                    .UpdateStudentTemplate(studentTemplateUpdateRequest)
                    .subscribe(response => {
                        if (response.isSuccess) {
                            this.messageService.add({
                                severity: "success",
                                summary: "İşlem Başarılı",
                                detail: "Kayıt Başarıyla Güncellendi."
                            });
                            this.loadData();
                            this.displayStudentDialog = false;
                        } else {
                            this.showErrors(response);
                        }
                    });
            } else {
                var studentTemplateCreateRequest = {
                    shuttleTemplateId: this.recordStudent.shuttleTemplateId,
                    studentId: this.selectedStudentId,
                    order: this.recordStudent.order,
                    lessonCount: this.recordStudent.lessonCount
                };
                this.service
                    .CreateStudentTemplate(studentTemplateCreateRequest)
                    .subscribe(response => {
                        if (response.isSuccess) {
                            this.messageService.add({
                                severity: "success",
                                summary: "İşlem Başarılı",
                                detail: "Kayıt Başarıyla Eklendi."
                            });
                            this.loadData();
                            this.displayStudentDialog = false;
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

    showShuttleTemplateDialogToDelete(record: any) {
       
        this.deleteShuttleTemplateId = record.id;
        var minute =
            record.time.getMinutes().toString().length == 1
                ? "0" + record.time.getMinutes().toString()
                : record.time.getMinutes();

		var time = record.time.getHours() + ":" + minute;
		var message=record.dayOfWeekName +" " +	time +" " +	record.locationRegion.name +" servis taslağını silmek istediğinize emin misiniz?";

        this.confirmationService.confirm({
            message:message,
            header: "Onaylıyor musunuz?",
            icon: "pi pi-question-circle",
            accept: () => {
                this.service.DeleteShuttleTemplate(this.deleteShuttleTemplateId)
                	.subscribe(response => {
                		if (response.isSuccess) {
                			this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Silindi' });
                			this.deleteShuttleTemplateId = 0;
                			this.loadData();
                		} else {
                			this.showErrors(response);
                		}
                	});
            },
            reject: () => {}
        });
        this.displayDialog = false;
    }
}
