import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd, Params, PRIMARY_OUTLET } from "@angular/router";
import "rxjs/add/operator/filter";
import { MenuItem } from "primeng/primeng";

interface IBreadcrumb {
    label: string;
    params?: Params;
    url: string;
    hasComponent: boolean;
}

@Component({
    selector: "breadcrumb",
    template: `
    <p-breadcrumb [model]="items" [home]="homeItem"></p-breadcrumb>
    `,
    styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit {

    public items: MenuItem[];
    public homeItem: MenuItem;
    public breadcrumbs: IBreadcrumb[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router
    ) {
        this.breadcrumbs = [];
    }

	ngOnInit() {
		const ROUTE_DATA_BREADCRUMB: string = "breadcrumb";

		this.homeItem = {
			label: "Ana Sayfa",
			routerLink: "/"
		}
		//subscribe to the NavigationEnd event
		this.router.events.filter(event => event instanceof NavigationEnd).subscribe(event => {

			//set breadcrumbs
			this.createBreadcrumbs();
		});
		this.createBreadcrumbs();
	}

	private createBreadcrumbs() {
		let root: ActivatedRoute = this.activatedRoute.root;
		this.breadcrumbs = this.getBreadcrumbs(root);
		if (this.breadcrumbs) {
			let breadcrumbs: MenuItem[] = [];
			this.homeItem = {
				label: "Ana Sayfa",
				routerLink: "/"
			};
			for (let breadcrumb of this.breadcrumbs) {
				let item: MenuItem = {
					label: breadcrumb.label,
					routerLink: breadcrumb.hasComponent ? breadcrumb.url : "",
					disabled: !breadcrumb.hasComponent
				};
				breadcrumbs.push(item);
			}
			this.items = breadcrumbs;
		}
	}

    private getBreadcrumbs(route: ActivatedRoute, url: string = "", breadcrumbs: IBreadcrumb[] = []): IBreadcrumb[] {
        const ROUTE_DATA_BREADCRUMB: string = "breadcrumb";

        //get the child routes
        let children: ActivatedRoute[] = route.children;

        //return if there are no more children
        if (children.length === 0) {
            return breadcrumbs;
        }

        //iterate over each children
        for (let child of children) {
            //verify primary route
            if (child.outlet !== PRIMARY_OUTLET) {
                continue;
            }

            //verify the custom data property "breadcrumb" is specified on the route
            if (!child.snapshot.data.hasOwnProperty(ROUTE_DATA_BREADCRUMB)) {
                return this.getBreadcrumbs(child, url, breadcrumbs);
            }

            //get the route's URL segment
            let routeURL: string = child.snapshot.url.map(segment => segment.path).join("/");

            //append route URL to URL
            url += `/${routeURL}`;

            //add breadcrumb
            let breadcrumb: IBreadcrumb = {
                label: child.snapshot.data[ROUTE_DATA_BREADCRUMB],
                params: child.snapshot.params,
                url: url,
                hasComponent: child.component ? true : false
            };
            breadcrumbs.push(breadcrumb);

            //recursive
            return this.getBreadcrumbs(child, url, breadcrumbs);
        }

        //we should never get here, but just in case
        return breadcrumbs;
    }

}
