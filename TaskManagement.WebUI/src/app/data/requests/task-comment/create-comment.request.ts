export default interface CreateNewCommentRequest {
  taskId: string;
  commentType: number;
  comment: string;
  reminderDate: Date;
}
