export default interface UpdateTaskRequest {
  taskId: string;
  description: string;
  status: number;
  type: number;
  assignedTo: string;
  nextActionDate: Date;
}
