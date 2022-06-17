import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'prime/message/messageservice';
import { Instructor, StudentInstructorRelation, Student, Branch } from 'shared/models';
import { StudentComponent } from 'individuals/student-management/student/student.component';
import { StudentBasePageComponent } from 'shared/components/basepages/student-basepage.component';
import { StudentInstructorRelationService } from 'shared/services/individuals/student-management/student-instructor-relation.service';
import { SelectItem, ConfirmationService } from 'primeng/primeng';
import { InstructorService } from 'shared/services/individuals/instructor-management/instructor.service';
import { BranchService, StudentService } from 'shared/services';

@Component({
	selector: 'student-instructor-relation',
	templateUrl: './student-instructor-relation.component.html'
})

export class StudentInstructorRelationComponent extends StudentBasePageComponent<StudentInstructorRelation, StudentInstructorRelationService> {


	studentInstructorRelations: StudentInstructorRelation[];
	studentId: number;

	public Instructors: SelectItem[];
	public selectedInstructor: number;
	public Branches: SelectItem[];
	public selectedBranch: number;
	constructor(public studentService: StudentService,
		public service: StudentInstructorRelationService,
		public messageService: MessageService,
		public instructorService: InstructorService,
		public branchService: BranchService,
		public confirmationService: ConfirmationService,

	) {
		super(StudentInstructorRelation, studentService, service, messageService, confirmationService);
	}

	ngOnInit() {
		this.baseForm = new FormGroup({
			// 'branch': new FormControl('', Validators.required),
			'instructor': new FormControl('', Validators.required),
			'priority': new FormControl('', Validators.required),
		});
		super.ngOnInit();

		this.Instructors = [];
		this.Instructors = [{ label: "Seçiniz", value: null }];
		this.instructorService.get()
			.subscribe(response => {
				var entities = (<Instructor[]>response.resultValue);
				for (let entity of entities) {
					this.Instructors.push({ label: entity.name + " " + entity.surname, value: entity.id });
				}
			});

		this.Branches = [];
		this.Branches = [{ label: "Seçiniz", value: null }];
		this.branchService.get()
			.subscribe(response => {
				var entities = (<Branch[]>response.resultValue);
				for (let entity of entities) {
					this.Branches.push({ label: entity.name, value: entity.id });
				}
			});
		this.record = new StudentInstructorRelation();
		this.record.instructor = new Instructor();
		this.record.branch = new Branch();
		this.record.student = new Student();
	}

	initData(rowData: StudentInstructorRelation[]) {
		if (rowData && rowData.length) {
			for (let record of rowData) {
				if (record.startDate) {
					record.startDate = new Date(record.startDate);
				}
			}
		}

		return rowData;
	}

	initAdd(record: StudentInstructorRelation) {
		this.selectedBranch = null;
		this.selectedInstructor = null;
	}

	initEdit(record: StudentInstructorRelation) {
		if (record.branch) {
			this.selectedBranch = record.branch.id;
		}
		if (record.instructor) {
			this.selectedInstructor = record.instructor.id;
		}
	}
}
