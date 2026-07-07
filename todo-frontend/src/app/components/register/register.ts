import { Component, ChangeDetectorRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})

export class RegisterComponent {
  username = '';
  email = '';
  password = '';
  successMessage = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router, private cdRef: ChangeDetectorRef) {}

  onSubmit(form: NgForm) {
    this.errorMessage = '';
    this.successMessage = '';

    if (form.invalid) {
      Object.values(form.controls).forEach(control => control.markAsTouched());
      return;
    }


    this.authService.register({ username: this.username, email: this.email, password: this.password }).subscribe({
      next: (response) => {
        this.successMessage = 'Registration successful! Redirecting...';
        this.cdRef.detectChanges();
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.successMessage = '';
        this.errorMessage = 'Registration error. Server-side error.';
        this.cdRef.detectChanges();
        console.error(err);
      }
    });
  }
}