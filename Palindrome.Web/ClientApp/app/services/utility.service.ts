import { Injectable } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup } from '@angular/forms'

@Injectable()
export class UtilityService {
    generateReactiveForm(control: AbstractControl, obj: any, callbackFn: ((propertyPath: string) => any) | null = null, stack = ''): AbstractControl {
        if (obj) {
            Object.keys(obj).forEach(property => {
                let newControl = control;
                if (obj[property] instanceof Array) {
                    newControl = new FormArray([]);
                    if (control instanceof FormGroup) {
                        control.addControl(property, newControl);
                    } else if (control instanceof FormArray) {
                        control.push(newControl);
                    }
                } else {
                    if (control instanceof FormGroup) {
                        if (obj[property] && Object.keys(obj[property]).length > 0 && typeof obj[property] !== 'string') {
                            newControl = new FormGroup({});
                            control.addControl(property, newControl);
                        } else {
                            control.addControl(property, new FormControl(obj[property]));
                        }
                    }
                    else if (control instanceof FormArray) {
                        newControl = new FormGroup({});
                        control.push(newControl);
                    }
                }

                if (typeof obj[property] !== 'string') {
                    let propPath = stack ? stack + '.' + property : property;
                    this.generateReactiveForm(newControl, obj[property], (propertyPath: string) => {
                        if (callbackFn) {
                            callbackFn(propertyPath);
                        }
                    }, propPath);
                }

                let propPath1 = stack ? stack + '.' + property : property;

                if (callbackFn) {
                    callbackFn(propPath1);
                }
            });
        }

        return control;
    }

    newGuid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}