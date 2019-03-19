import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Palindrome } from '../viewmodels/palindrome';

@Injectable()
export class PalindromeService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }

    add(vm: Palindrome): Observable<any> {
        return this.http.post(this.baseUrl + 'api/palindrome', JSON.stringify(vm),
            new RequestOptions({ headers: new Headers({ 'Content-Type': 'application/json' }) }))
            .map((data) => data.json());
    }

    getAll(): Observable<any> {
        return this.http.get(this.baseUrl + 'api/palindrome',
            new RequestOptions({ headers: new Headers({ 'Content-Type': 'application/json' }) }))
            .map((data) => data.json());
    }
}