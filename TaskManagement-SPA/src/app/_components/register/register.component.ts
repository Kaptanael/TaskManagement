import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, Validators, FormBuilder, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Message } from 'primeng/api';
import { AuthService } from '../../_services/auth.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  growlMessage: Message[] = [];
  registerFormGroup: FormGroup;
  @Input() isAdmin = false;

  constructor(private fb: FormBuilder, private router: Router, private authService: AuthService, private location: Location) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerFormGroup = this.fb.group({
      firstName: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64)]],
      lastName: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64)]],
      email: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64), Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      retypePassword: ['', Validators.required],
      mobileNumber: ['', [Validators.required, this.noWhitespaceValidator, Validators.minLength(11), Validators.maxLength(14)]],
      address: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(512)]]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(fg: FormGroup) {
    return fg.get('password').value === fg.get('retypePassword').value ? null : { mismatch: true };
  }

  noWhitespaceValidator(control: AbstractControl) {
    if (control && control.value && !control.value.replace(/\s/g, '').length) {
      control.setValue('');
    }
    return null;
  }

  register() {
    if (this.registerFormGroup.valid) {      
      const userModel: any = {
        id: 0,
        firstName: this.registerFormGroup.get('firstName').value,
        lastName: this.registerFormGroup.get('lastName').value,
        email: this.registerFormGroup.get('email').value,
        password: this.registerFormGroup.get('password').value,
        mobileNumber: this.registerFormGroup.get('mobileNumber').value,
        address: this.registerFormGroup.get('address').value,
        role: (this.isAdmin) ? 'Admin' : 'Regular'
      };
      this.authService.register(userModel).subscribe(() => {
        this.registerFormGroup.reset({selfOnly:true});
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'success', summary: 'Registration successful', detail: '' });
      }, error => {
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'error', summary: error, detail: '' });
      },
        () => {
          if (!this.isAdmin) {
            this.router.navigate(['/login']);
          }
        });
    }
  }

  onEmailBlur(email) {    
    if(this.registerFormGroup.get('email').value){
      this.authService.isExistEmail(email.trim())
        .subscribe(responeData => {
          if (responeData.body === true) {
            this.registerFormGroup.controls.email.setErrors({ invalid: true });
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Email is already in use', detail: '' });
          }
        },
          error => {            
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Failed to check duplicate email', detail: '' });
          });
    }
  }

  goBack(){
    this.location.back();
  }
}
