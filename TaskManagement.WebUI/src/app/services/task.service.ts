import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import CreateTaskRequest from '../data/requests/task/create-task.request';
import UpdateTaskRequest from '../data/requests/task/update-task.request';
import TaskModel from '../data/models/task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  baseUrl: string = 'https://localhost:44340/api';

  constructor(private httpClient: HttpClient) {}

  getById(id: string) {
    this.httpClient
      .get(`${this.baseUrl}/Task/GetById/${id}`)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  getAll() {
    this.httpClient.get(`${this.baseUrl}/Task/GetAll`).subscribe((result) => {
      console.warn(result);
    });
  }

  create(payload: CreateTaskRequest) {
    this.httpClient
      .post<CreateTaskRequest>(`${this.baseUrl}/Task/Create`, payload)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  update(payload: UpdateTaskRequest) {
    this.httpClient
      .put<UpdateTaskRequest>(`${this.baseUrl}/Task/Update`, payload)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  remove(taskId: string) {
    this.httpClient
      .delete(`${this.baseUrl}/Task/Remove/${taskId}`)
      .subscribe((result) => {
        console.warn(result);
      });
  }
}
