import { Component, AfterViewInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { UtilityService } from '../../services/utility.service';
import { PalindromeService } from '../../services/palindrome.service';
import { Palindrome } from '../../viewmodels/palindrome';
import { PalindromeValidator } from '../../validators/palindrome.validator';

@Component({
    selector: 'palidrome',
    templateUrl: './palindrome.component.html'
})
export class PalindromeComponent implements AfterViewInit {
    public form: FormGroup;
    public showSuccess: boolean = false;
    public showError: boolean = false;
    private errorMessages: any = [];
    public id: string;
    public triedToSubmitForm: boolean = false;
    private object: Object = Object;
    public phrases: Array<Palindrome> = new Array<Palindrome>();

    constructor(private utilityService: UtilityService, private palindromeService: PalindromeService) {
        this.form = <FormGroup>this.utilityService.generateReactiveForm(new FormGroup({}), new Palindrome());
        PalindromeValidator.apply(this.form);
    }

    ngAfterViewInit() {
        this.palindromeService.getAll()
            .subscribe(
                data => {
                    //Make all model properties as pristine
                    this.phrases = data;
                }, (err) => this.onError(err));
    }

    private onError(err: any)
    {
        if (!err.ok && err._body) {
            var responseMessage = JSON.parse(err._body);
            if (responseMessage.errors) {
                this.errorMessages = responseMessage.errors;
            } else {
                this.errorMessages = [err.statusText];
            }
        } else {
            this.errorMessages = [err.statusText];
        }

        this.showSuccess = false;
        this.showError = true;
    }

    public submit() {
        this.triedToSubmitForm = true;
        if (!this.form.valid) {
            return;
        }

        this.palindromeService.add(<Palindrome>this.form.value)
            .subscribe(
            data => {
                //Make all model properties as pristine
                this.id = data;
                this.showSuccess = true;
                this.showError = false;

                let vm = <Palindrome>this.form.value;
                vm.id = this.id;
                this.phrases.push(vm);
            }, (err) => this.onError(err));
    }

    public submitAgain() {
        this.form.reset();
        this.triedToSubmitForm = false;
        this.showSuccess = false;
    }
}