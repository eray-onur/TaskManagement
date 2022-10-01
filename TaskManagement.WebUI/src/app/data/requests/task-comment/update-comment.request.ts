export default interface UpdateCommentRequest {
  taskCommentId: string;
  taskId: string;
  commentType: number;
  comment: string;
  reminderDate: Date;
}
