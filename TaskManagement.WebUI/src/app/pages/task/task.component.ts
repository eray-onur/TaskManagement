import { Component, OnInit } from '@angular/core';
import { TaskCommentService } from 'src/app/services/task-comment.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss'],
})
export class TaskComponent implements OnInit {
  constructor(
    private taskService: TaskService,
    private taskCommentService: TaskCommentService
  ) {}

  ngOnInit(): void {
    const x = this.taskService.getAll();
  }
}
