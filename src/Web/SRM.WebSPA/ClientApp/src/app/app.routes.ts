import { Routes, RouterModule } from "@angular/router";
import { ModuleWithProviders } from "@angular/core";
import {
    AppLayoutComponent,
    LoginLayoutComponent
} from "application-srm/layouts";
import { HomeComponent } from "application-srm/home";
import { AuthGuard } from "shared/security";
import {
    StudentComponent,
    StudentSearchComponent
} from "individuals/student-management";
import {
    ObstacleTypeComponent,
    HospitalComponent
} from "individuals/parameters";
import { ExamplesAppLayoutComponent } from "examples/layouts";
import { LoginComponent } from "security/login";

import * as examples from "examples/demo";
import { BranchComponent } from "courses/parameters";
import {
    InstructorComponent,
    InstructorSearchComponent
} from "individuals/instructor-management";
import {
    ShuttleStudentAdviceComponent,
    ShuttleListComponent,
    ShuttleOperationLocationComponent
} from "shuttles/operation-management";
import {
    LocationRegionComponent,
    StudentServiceComponent
} from "shuttles/parameters";
import { ShuttleTemplateListComponent } from "shuttles/template-management";
import { HelpPageComponent } from "./help/help-page/help-page.component";
import { StudentParticipationStatusReport } from "./reports";
import { PageComponent, UnauthorizedComponent } from "./application-management";
import { UserComponent } from "security/accounts/user/user.component";
import { RoleComponent } from "security/accounts/role/role.component";
import { ChangePasswordComponent } from "security/accounts/change-password/change-password.component";

export const routes: Routes = [
    {
        path: "",
        component: AppLayoutComponent,
        children: [
            { path: "", component: HomeComponent, canActivate: [AuthGuard] },

            {
                path: "student-affairs",
                children: [
                    {
                        path: "student",
                        component: StudentComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğrenci Sayfası"
                        }
                    },
                    {
                        path: "student/search",
                        component: StudentSearchComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğrenci Arama"
                        }
                    },
                    {
                        path: "student/:id",
                        component: StudentComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğrenci Sayfası"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Öğrenci İşlemleri"
                }
            },

            {
                path: "shuttle-affairs",
                children: [
                    {
                        path: "shuttle-student-advice",
                        component: ShuttleStudentAdviceComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Önerilen Öğrencilerin Listesi"
                        }
                    },
                    {
                        path: "shuttle-list",
                        component: ShuttleListComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Servis Listesi"
                        }
                    },
                    {
                        path: "shuttle-template-list",
                        component: ShuttleTemplateListComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Servis Taslak Listesi"
                        }
                    },
                    {
                        path: "shuttle-template-list",
                        component: ShuttleTemplateListComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Servis Taslak Listesi"
                        }
                    },
                    {
                        path: "shuttle-operation-student-location/:id",
                        component: ShuttleOperationLocationComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Servis Konum Listesi"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Servis İşlemleri"
                }
            },

            {
                path: "instructor-affairs",
                children: [
                    {
                        path: "instructor",
                        component: InstructorComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğretmen Sayfası"
                        }
                    },
                    {
                        path: "instructor/search",
                        component: InstructorSearchComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğretmen Arama"
                        }
                    },
                    {
                        path: "instructor/:id",
                        component: InstructorComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğretmen Sayfası"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Öğretmen İşlemleri"
                }
            },
            {
                path: "parameters",
                children: [
                    {
                        path: "obstacletype",
                        component: ObstacleTypeComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Engel Tip Tanım"
                        }
                    },
                    {
                        path: "hospital",
                        component: HospitalComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Hastane Tanım"
                        }
                    },
                    {
                        path: "branch",
                        component: BranchComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Branş Tanım"
                        }
                    },
                    {
                        path: "location-region",
                        component: LocationRegionComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Bölge Tanım"
                        }
                    },
                    {
                        path: "student-service",
                        component: StudentServiceComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Servis Tanım"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Parametreler"
                }
            },
            {
                path: "admin-affairs",
                children: [
                    {
                        path: "page",
                        component: PageComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Sayfa Tanım"
                        }
                    },
                    {
                        path: "user",
                        component: UserComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Kullanıcı Tanım"
                        }
                    },
                    {
                        path: "role",
                        component: RoleComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Rol Tanım"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Öğrenci İşlemleri"
                }
            },
            {
                path: "security",
                children: [
                    {
                        path: "change-password",
                        component: ChangePasswordComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Şifre Değiştir"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Güvenlik"
                }
            },
            {
                path: "reports",
                children: [
                    {
                        path: "student-participation-status",
                        component: StudentParticipationStatusReport,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Öğrenci Katılım Durum Raporu"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Raporlar"
                }
            },
            {
                path: "unouthorized",
                component: UnauthorizedComponent,
                data: {
                    breadcrumb: "Yetkisiz Erişim"
                }
            },
            {
                path: "help",
                children: [
                    {
                        path: "help-page",
                        component: HelpPageComponent,
                        canActivate: [AuthGuard],
                        data: {
                            breadcrumb: "Yardım Sayfası"
                        }
                    }
                ],
                data: {
                    breadcrumb: "Yardım"
                }
            }
        ]
    },
    {
        path: "examples",
        component: ExamplesAppLayoutComponent,
        children: [
            {
                path: "",
                component: examples.DashboardDemoComponent,
                canActivate: [AuthGuard]
            },
            { path: "sample", component: examples.SampleDemoComponent },
            { path: "forms", component: examples.FormsDemoComponent },
            { path: "data", component: examples.DataDemoComponent },
            { path: "panels", component: examples.PanelsDemoComponent },
            { path: "overlays", component: examples.OverlaysDemoComponent },
            { path: "menus", component: examples.MenusDemoComponent },
            { path: "messages", component: examples.MessagesDemoComponent },
            { path: "misc", component: examples.MiscDemoComponent },
            { path: "empty", component: examples.EmptyDemoComponent },
            { path: "charts", component: examples.ChartsDemoComponent },
            { path: "file", component: examples.FileDemoComponent },
            { path: "utils", component: examples.UtilsDemoComponent },
            {
                path: "documentation",
                component: examples.DocumentationComponent
            }
        ]
    },
    {
        path: "",
        component: LoginLayoutComponent,
        children: [
            {
                path: "login",
                component: LoginComponent
            }
        ]
    }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
