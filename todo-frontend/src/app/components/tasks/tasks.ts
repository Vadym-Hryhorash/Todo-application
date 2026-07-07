import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TaskService } from '../../services/task.service';
import { CategoryService } from '../../services/category.service';
import { AuthService } from '../../services/auth.service';
import { Task, Category } from '../../models/app.models';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css'
})
export class TasksComponent implements OnInit {
  tasks: Task[] = [];
  categories: Category[] = [];
  
  searchQuery = '';
  selectedCategoryId: number | null = null;
  pageNumber = 1;
  pageSize = 10;

  newCategoryName = '';
  newTask: Task = {
    title: '',
    description: '',
    dueDate: new Date().toISOString().split('T')[0],
    isCompleted: false,
    categoryId: null
  };

  private taskSub: Subscription | null = null;

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService,
    private authService: AuthService,
    private router: Router,
    private cdRef: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadCategories();
    this.loadTasks();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(res => this.categories = res);
    this.cdRef.detectChanges();
  }

  loadTasks() {
    if (this.taskSub) {
      this.taskSub.unsubscribe();
    }

    this.taskSub = this.taskService.getTasks(
      this.pageNumber, 
      this.pageSize, 
      this.selectedCategoryId ?? undefined, 
      this.searchQuery
    ).subscribe({
      next: (res) => {
        this.tasks = res;
        this.cdRef.detectChanges();
      },
      error: (err) => {
        console.error('Error:', err);
      }
    });
  }

  onSearch() {
    this.pageNumber = 1;
    this.loadTasks();
  }

  filterByCategory(categoryId: number | null) {
    this.selectedCategoryId = categoryId;
    this.pageNumber = 1;
    this.loadTasks();
  }

  nextPage() {
    this.pageNumber++;
    this.loadTasks();
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadTasks();
    }
  }

  createCategory() {
    if (!this.newCategoryName.trim()) return;
    const cat: Category = { name: this.newCategoryName };
    
    this.categoryService.createCategory(cat).subscribe(res => {
      this.categories.push(res);
      this.newCategoryName = '';
      this.cdRef.detectChanges();
    });
  }

  createTask() {
    if (!this.newTask.title.trim()) return;

    this.taskService.createTask(this.newTask).subscribe(res => {
      this.loadTasks();
      this.newTask.title = '';
      this.newTask.description = '';
    });
  }

  toggleTask(task: Task) {
    task.isCompleted = !task.isCompleted;
    if (task.id) {
      this.taskService.updateTask(task.id, task).subscribe();
      this.cdRef.detectChanges();
    }
  }

  deleteTask(id: number | undefined) {
    if (!id) return;
    this.taskService.deleteTask(id).subscribe(() => {
      this.loadTasks();
    });
  }

  showDeleteModal = false;
  categoryToDeleteId: number | null = null;

  openDeleteModal(id: number | undefined, event: Event) {
    event.stopPropagation(); 
    if (!id) return;
    
    this.categoryToDeleteId = id;
    this.showDeleteModal = true;
  }

  closeModal() {
    this.showDeleteModal = false;
    this.categoryToDeleteId = null;
  }

  confirmDelete() {
    if (!this.categoryToDeleteId) return;

    this.categoryService.deleteCategory(this.categoryToDeleteId).subscribe({
      next: () => {
        this.categories = this.categories.filter(c => c.id !== this.categoryToDeleteId);

        if (this.selectedCategoryId === this.categoryToDeleteId) {
          this.selectedCategoryId = null;
          this.pageNumber = 1;
          this.loadTasks();
        }

        this.closeModal();
        this.cdRef.detectChanges(); 
      },
      error: (err) => {
        console.error('Error:', err);
        this.closeModal();
      }
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}