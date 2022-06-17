import { BaseModel } from "shared/models/base.model";
import { Lesson } from "shared/models/courses/lesson/lesson.model";
import { Instructor } from "shared/models/individuals/instructor-management/instructor/instructor.model";

export class LessonSession extends BaseModel {
    lesson: Lesson;
    instructor: Instructor;
    header: string;
    content: string;
    startDate: Date;
    constructor() {
        super()
        this.lesson = new Lesson();
    }
}
