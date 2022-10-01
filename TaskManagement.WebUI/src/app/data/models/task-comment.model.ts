export default interface TaskCommentModel {
  commentId: string;
  taskId: string;
  dateAdded: Date;
  commentType: number;
  comment: string;
  reminderDate: Date;
}
