import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'shared/services';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(public auth: AuthService, public router: Router) { }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        if (!this.auth.Authenticated()) {
			this.auth.logoutUser(state.url);
            //this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
            return false;
		}

        return this.auth.checkUserPagePermission(route.routeConfig.path).map((permissionState: any) => {
            if (!permissionState || !permissionState.isSuccess) {
                // if (permissionState) {
                //     this.logger.warn(permissionState.error);
                // }
				this.router.navigate(['/unouthorized']);
                return false;
            }
            return permissionState.isSuccess;
        });
    }
}