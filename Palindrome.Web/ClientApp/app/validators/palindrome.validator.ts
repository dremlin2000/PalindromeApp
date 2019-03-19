import { FormGroup, FormControl, AbstractControl, ValidatorFn, Validators } from '@angular/forms';


export class PalindromeValidator {
    static apply(group: FormGroup): any {
        let prop = group.get("phrase");
        if (prop) {
            prop.setValidators([
                Validators.required,
            ]);
        }
    }
}