export interface Category {
  id?: number;
  name: string;
}

export interface Task {
  id?: number;
  title: string;
  description: string;
  dueDate: string | Date;
  createdAt?: string | Date;
  isCompleted: boolean;
  categoryId?: number | null;
  category?: Category;
}