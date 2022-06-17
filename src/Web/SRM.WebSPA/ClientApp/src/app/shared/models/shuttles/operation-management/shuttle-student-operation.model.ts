import { BaseModel } from "shared/models/base.model";
import { ShuttleOperation } from "shared/models/shuttles/operation-management/shuttle-operation.model";
import { Student } from "shared/models/individuals/student-management/student/student.model";

export class ShuttleStudentOperation extends BaseModel {
    shuttleOperation: ShuttleOperation;
    student: Student;
    status?: boolean;
    IsCompension: boolean;
}
