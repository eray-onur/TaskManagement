export default interface TaskModel {
  id: string;
  createdDate: Date;
  requiredByDate: Date;
  description: string;
  status: number;
  type: number;
  assignedTo: string;
  nextActionDate: Date;
}
