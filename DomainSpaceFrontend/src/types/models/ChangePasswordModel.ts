export class ChangePasswordModel {
  oldPassword?: string;
  newPassword?: string;
  confirmNewPassword?: string;

  constructor({
    oldPassword,
    newPassword,
    confirmNewPassword,
  }: ChangePasswordModel) {
    this.oldPassword = oldPassword;
    this.newPassword = newPassword;
    this.confirmNewPassword = confirmNewPassword;
  }
}
