import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { BasePageComponent } from 'shared/components';
import { Lesson, LessonSession, Student } from 'shared/models';
import { StudentService, LessonService } from 'shared/services';
import { StudentComponent } from 'individuals/student-management/student/student.component';

@Component({
	selector: 'student-evolution',
	templateUrl: './student-evolution.component.html'
})

export class StudentEvolutionComponent extends BasePageComponent {
	lessons: Lesson[];
	selectedLesson: Lesson;
	lessonSessions: LessonSession[];
	searchDate: Date;
	studentId: number;
	constructor(
		public studentComponent: StudentComponent,
		public service: StudentService,
		public lessonService: LessonService,
		public messageService: MessageService
	) {
		super(messageService);
		this.searchDate = new Date();
		this.service.studentLoaded$.subscribe(studentId => {
			this.studentId = Number(studentId);
			this.loadLesson(this.studentId);
		});
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			'studentName': new FormControl(''),
			'studentSurname': new FormControl('', Validators.required),
			'lessonName': new FormControl('', Validators.required),
			'lessonSessionHeader': new FormControl('', Validators.required),
			'lessonSessionContent': new FormControl('', Validators.required),
		});
		super.ngOnInit();
		this.record = new LessonSession();
		this.record.lesson = new Lesson();
		this.record.lesson.student = new Student();
	}
	loadLesson(studentId) {
		// this.service.getStudentLessons(studentId)
		//     .subscribe(response => {
		//         this.lessons = (<ILesson[]>response.resultValue);
		//     });
	}
	search(id) {
		var searchDateString = this.searchDate != null ? this.searchDate.toUTCString() : null;
		var requestObject = { id: id, searchDate: searchDateString }
		this.lessonService.getLessonSessions(requestObject)
			.subscribe(response => {
				this.lessonSessions = (<LessonSession[]>response.resultValue);

				for (var i = 0; i < this.lessonSessions.length; i++) {
					var item = this.lessonSessions[i];
					if (item.startDate) {
						item.startDate = new Date(item.startDate);
					}
				}
			});
	}

	lessonSelect(event) {
		var lesson: Lesson = event.value;
		this.search(lesson.id);
	}

	loadLessonSessions(lessonId) {
		this.lessonService.getLessonSessions(lessonId)
			.subscribe(response => {
				this.lessonSessions = (<LessonSession[]>response.resultValue);

				for (var i = 0; i < this.lessonSessions.length; i++) {
					var item = this.lessonSessions[i];
					if (item.startDate) {
						item.startDate = new Date(item.startDate);
					}
				}
			});
	}



	public displayDialog: boolean;
	public displayDeleteDialog: boolean;
	public newRecord: boolean;
	record: LessonSession;
	public deleteId: any;

	showDialogToEdit(record: LessonSession) {
		this.newRecord = false;
		this.record = Object.assign({}, record);
		this.displayDialog = true;
	}

	save() {
		if (this.newRecord == false) {
			this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Güncellendi.' });
			// this.service.put(this.record.id, this.record)
			//     .subscribe(response => {
			//         if (response.isSuccess) {
			//             this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Güncellendi.' });
			//             this.loadData();
			//         } else {
			//             this.showErrors(response);
			//         }
			//     });
		} else {
			this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
			// this.service.post(this.record)
			//     .subscribe(response => {
			//         if (response.isSuccess) {
			//             this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Eklendi.' });
			//             this.loadData();
			//         } else {
			//             this.showErrors(response);
			//         }
			//     });
		}
		this.displayDialog = false;
	}

	showDialogToAdd() {
		this.newRecord = true;
		this.deleteId = 0;
		this.record = new LessonSession();
		this.displayDialog = true;
	}
	cancel() {
		// this.record = this.getNew();
		this.displayDialog = false;
	}

	showDialogToDelete(record: LessonSession) {
		this.deleteId = record.id;
		this.displayDeleteDialog = true;
	}


	okDelete(isDeleteConfirm: boolean) {
		if (isDeleteConfirm) {
			this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Silindi' });
			// this.service.delete(this.deleteId)
			//     .subscribe(response => {
			//         if (response.isSuccess) {

			//             this.messageService.add({ severity: 'success', summary: 'İşlem Başarılı', detail: 'Kayıt Başarıyla Silindi' });
			//             this.deleteId = 0;
			//             this.loadData();
			//         } else {
			//             this.showErrors(response);
			//         }
			//     });
		}
		this.displayDeleteDialog = false;
		this.displayDialog = false;
	}
}
