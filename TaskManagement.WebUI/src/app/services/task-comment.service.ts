import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import CreateNewCommentRequest from '../data/requests/task-comment/create-comment.request';
import UpdateCommentRequest from '../data/requests/task-comment/update-comment.request';

@Injectable({
  providedIn: 'root',
})
export class TaskCommentService {
  baseUrl: string = 'http://localhost:5000';

  constructor(private httpClient: HttpClient) {}

  getById(id: string) {
    this.httpClient
      .get(`${this.baseUrl}/taskComment/getById/${id}`)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  getAll() {
    this.httpClient
      .get(`${this.baseUrl}/taskComment/getAll`)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  create(payload: CreateNewCommentRequest) {
    this.httpClient
      .post<CreateNewCommentRequest>(
        `${this.baseUrl}/taskComment/create`,
        payload
      )
      .subscribe((result) => {
        console.warn(result);
      });
  }

  update(payload: UpdateCommentRequest) {
    this.httpClient
      .put<UpdateCommentRequest>(`${this.baseUrl}/taskComment/create`, payload)
      .subscribe((result) => {
        console.warn(result);
      });
  }

  remove(taskId: string) {
    this.httpClient
      .delete(`${this.baseUrl}/taskComment/remove/${taskId}`)
      .subscribe((result) => {
        console.warn(result);
      });
  }
}
