import { Individual } from "shared/models/individuals/individual.model";
import { Branch } from "shared/models/courses/parameters/branch/branch.model";

export class Instructor extends Individual {
    branches: Branch[];
    phone: string;
    email: string;
    address: string;
    hireDate?: Date;
    isActive: boolean;
    constructor()
    {
        super()
        this.branches=[];
    }
}
