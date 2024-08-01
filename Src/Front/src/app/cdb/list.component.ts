import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';

import { AlertService } from '@app/_services';
import { CDBService } from '@app/_services/cdb.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({ templateUrl: 'list.component.html' })


export class ListComponent implements OnInit {
    form!: FormGroup;
    cdbList?: any[];
    aguardando: boolean = false;
    // valor?: number;
    // meses?: number;

    constructor(private cdbService: CDBService, private route: ActivatedRoute, private router: Router,  private formBuilder: FormBuilder, private alertService: AlertService) {}

    ngOnInit() {
        console.log('Called Constructor');

        this.form = this.formBuilder.group({
            valor: ['', Validators.required],
            meses: ['', Validators.required]
        });
    }

    calcular(){
        let x = this.form.value;
        
        this.aguardando = true;

        this.cdbService.getAll(this.form.value.valor!, this.form.value.meses!)
        
        .pipe(map(val => { 
            return (<any>val).data
        }))
        .subscribe({
            next: (response) => {
                this.aguardando = false;
                this.cdbList = response;
            },
            error: (error) => {
                this.alertService.error(error);
                this.aguardando = false;

            }
          })
        // .subscribe(val => { 
        //     this.cdbList = val;
        // })
    }

    limpar(){
        this.cdbList = undefined;
    }
}