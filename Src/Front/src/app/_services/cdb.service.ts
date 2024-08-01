import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { CDB } from '@app/_models/cdb';
import { BaseService, StorangeEnum } from './base.service';

@Injectable({ providedIn: 'root' })
export class CDBService extends BaseService<CDB> {
    private cDBSubject: BehaviorSubject<CDB | null>;
    public cdb: Observable<CDB | null>;

    constructor(
        private router: Router,
        httpClient: HttpClient,
    ) {
        super(httpClient);
        this.cDBSubject = new BehaviorSubject(JSON.parse(localStorage.getItem(StorangeEnum.session.toString())!));
        this.cdb = this.cDBSubject.asObservable();
    }

    public get userValue() {
        return this.cDBSubject.value;
    }

    add(params: any) {
        return this.post('/v1/cdb', {... params})
    }

    getAll(valor: number, meses: number) {
        return this.get(`/v1/cdb/calcular?valor=${valor}&meses=${meses}`)
    }

    getById(id: any) {
        return this.get(`/v1/cdb/${id}`)
        //return this.httpClient.get<CDB>(`${environment.apiUrl}/v1/cdb/${id}`);
    }

    update(id: number, params: any) {
        return this.put(`/v1/cdb/${id}`, {... params})
            .pipe(map(x => {
                return x;
            }));
    }

    override delete(id: any) {
        return super.delete(id);
    }
}