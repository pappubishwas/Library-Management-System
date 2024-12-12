import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../shared/services/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  hidePwdContent: boolean = true;
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private snackBar: MatSnackBar
  ) {
    this.registerForm = fb.group(
      {
        userName: fb.control('', [Validators.required]),
        firstName: fb.control('', [Validators.required]),
        lastName: fb.control('', [Validators.required]),
        email: fb.control('', [Validators.required, Validators.email]),
        mobileNumber: fb.control('', [
          Validators.required,
          Validators.pattern('^[0-9]{10}$'), 
        ]),
        password: fb.control('', [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern('^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).+$'), 
        ]),
        rpassword: fb.control('', [Validators.required]),
      },
      { validators: this.passwordMatchValidator }
    );
  }

  // Custom validator to check if passwords match
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const rpassword = form.get('rpassword')?.value;
    return password === rpassword ? null : { mismatch: true };
  }

  checkUsernameAndEmail() {
    const username = this.registerForm.get('userName')?.value;
    const email = this.registerForm.get('email')?.value;

    this.apiService.getUser().subscribe((userList) => {
      let isUsernameTaken = false;
      let isEmailTaken = false;

      for (let user of userList) {
        if (user.username.toLowerCase() === username.toLowerCase()) {
          isUsernameTaken = true;
          break;
        }
        if (user.email.toLowerCase() === email.toLowerCase()) {
          isEmailTaken = true;
          break;
        }
      }

      if (isUsernameTaken) {
        this.snackBar.open('Username is already taken', 'OK', { duration: 3000 });
        return;
      }
      if (isEmailTaken) {
        this.snackBar.open('Email is already taken', 'OK', { duration: 3000 });
        return;
      }
      this.register();
    });
  }

  register() {
    let user = {
      userName: this.registerForm.get('userName')?.value,
      firstName: this.registerForm.get('firstName')?.value,
      lastName: this.registerForm.get('lastName')?.value,
      email: this.registerForm.get('email')?.value,
      mobileNumber: this.registerForm.get('mobileNumber')?.value,
      password: this.registerForm.get('password')?.value,
    };

    this.apiService.register(user).subscribe({
      next: (res) => {
        this.snackBar.open('Registration successful', 'OK');
        this.registerForm.reset();
      },
      error: (err) => {
        this.snackBar.open('Registration failed', 'OK');
      },
    });
  }

  // Submit handler
  onSubmit() {
    if (this.registerForm.valid) {
      this.checkUsernameAndEmail();
    } else {
      this.snackBar.open('Please fill all fields correctly', 'OK');
    }
  }
}
