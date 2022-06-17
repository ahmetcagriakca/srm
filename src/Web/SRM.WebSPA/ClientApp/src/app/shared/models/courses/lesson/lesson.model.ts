import { BaseModel } from "shared/models/base.model";
import { Branch } from "shared/models/courses/parameters/branch/branch.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";

export class Lesson extends BaseModel {
    name: string;
    branch: Branch;
    student: Student;
    constructor()
    {
        super()
        this.branch=new Branch();
        this.student=new Student();
    }
}
