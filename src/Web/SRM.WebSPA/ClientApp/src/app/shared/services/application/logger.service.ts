import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Rx';
import { HttpClient } from '@angular/common/http';
import { Injector } from '@angular/core';
import { LocationStrategy } from '@angular/common';
import { PathLocationStrategy } from '@angular/common';
import { StorageService } from 'shared/services/application/storage.service';
import { LogLevel, Log } from 'shared/models';

@Injectable()
export class LoggerService {
    _serviceApi: string = '/Logger/';

    constructor(public http: HttpClient, private storage: StorageService, private injector: Injector) {
        // super(http, storage)
    }

    private log(log) {
        log.channel = "Angular"
        return this.http.post(this._serviceApi + "Post", log)
            .map(response => <any>(<Response>response));
	}
	
    private getStack(error?: any) {
        if (error) {
            return error.stack ? error.stack : error.toString();
        }
        return '';
    }

    private getUrl(error?: any) {
        const location = this.injector.get(LocationStrategy);
        return location instanceof PathLocationStrategy
            ? location.path() : (location._platformLocation ? location._platformLocation.location.href : '');
    }
    getLog(message, logLevel: LogLevel, error?: any): Log {
        return {
            message: message,
            level: logLevel,
            url: this.getUrl(),
            stack: this.getStack(error)
        };
    }
    debug(message: string, error?: any) {
        let log: Log = this.getLog(message, LogLevel.DEBUG, error);

        this.log(log)
            .subscribe(response => {
            });
    }

    info(message: string, error?: any) {

        let log: Log = this.getLog(message, LogLevel.INFO, error);
        this.log(log)
            .subscribe(response => {
                var check = response;
            });
    }

    warn(message: string, error?: any) {
        let log: Log = this.getLog(message, LogLevel.WARN, error);
        this.log(log)
            .subscribe(response => {
                var check = response;
            });
    }

    error(message: string, error?: any) {
        let log: Log = this.getLog(message, LogLevel.ERROR, error);
        return this.log(log);
    }

    fatal(message: string, error?: any) {
        let log: Log = this.getLog(message, LogLevel.FATAL, error);
        this.log(log)
            .subscribe(response => {
                var check = response;
            });
    }
}