import { BaseModel } from "shared/models/base.model";
import { Instructor } from "shared/models/individuals/instructor-management/instructor/instructor.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";
import { Branch } from "shared/models/courses/parameters/branch/branch.model";

export class StudentInstructorRelation extends BaseModel {
    student: Student;
    instructor: Instructor;
    branch: Branch;
    priority: number;
    startDate: Date;
    constructor() {
        super()
        this.student=new Student();
        this.instructor=new Instructor();
        this.branch=new Branch();
    }
}