import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import CreateTaskRequest from 'src/app/data/requests/task/create-task.request';
import { TaskCommentService } from 'src/app/services/task-comment.service';
import { TaskService } from 'src/app/services/task.service';

interface TaskType {
  title: string;
  value: number;
}

interface AssignableUser {
  name: string;
  id: string;
}

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss'],
})
export class CreateTaskComponent implements OnInit {
  constructor(
    private taskService: TaskService,
    private taskCommentService: TaskCommentService
  ) {}

  datepicker: any;
  taskTypes: Array<TaskType> = [{ title: 'DEVELOPMENT', value: 1 }];
  assignableUsers: Array<AssignableUser> = [
    { name: 'John Doe', id: '7173d8fc-6c3f-4e39-8b07-becb8ee089ca' },
    { name: 'Jane Doe', id: '654c78f6-da55-4f60-a575-b4ff1f11c081' },
  ];

  createTaskForm = new FormGroup({
    description: new FormControl(),
    type: new FormControl(),
    assignedTo: new FormControl(),
    nextActionDate: new FormControl(),
  });

  ngOnInit(): void {}

  createTask() {
    const payload: CreateTaskRequest = {
      description: this.createTaskForm.controls.description.value,
      type: this.createTaskForm.controls.type.value,
      assignedTo: this.createTaskForm.controls.assignedTo.value,
    };
    this.taskService.create(payload);
  }
}
