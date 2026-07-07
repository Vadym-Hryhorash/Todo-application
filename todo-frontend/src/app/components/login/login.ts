import { Component, ChangeDetectorRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router, private cdRef: ChangeDetectorRef) {}

  onSubmit(form: NgForm) {
    this.errorMessage = '';

    if (form.invalid) {
      Object.values(form.controls).forEach(control => control.markAsTouched());
      return; 
    }

    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (response: any) => {
        if (response.token) {
          this.authService.saveToken(response.token);
          this.router.navigate(['/tasks']);
        }
      },
      error: (err) => {
        this.errorMessage = 'Wrong email or password. Please try again.';
        this.cdRef.detectChanges();
        console.error('Login error:', err);
      }
    });
  }
}
