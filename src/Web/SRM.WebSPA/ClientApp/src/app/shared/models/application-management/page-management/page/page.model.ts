import { ParameterBaseModel } from "shared/models/base.model";
import { PageRole } from "./pagerole.model";

export class Page extends ParameterBaseModel {
	name?;
	url?;
	componentName?;
	order?;
	icon?;
	showOnMenu?;
	parentId?;
	parent: Page;
	children;
	pageRoleIds: number[];
	pageRoles: PageRole[];
	isParent?;
    constructor()
    {
        super()
        this.pageRoles=[];
    }
}
