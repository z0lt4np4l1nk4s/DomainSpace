export enum RoleEnum {
  Admin = "Admin",
  Moderator = "Moderator",
  User = "User",
}

export const AllRoleEnums: RoleEnum[] = [
  RoleEnum.Admin,
  RoleEnum.Moderator,
  RoleEnum.User,
];

export const ModeratorRoleEnums: RoleEnum[] = [
  RoleEnum.Admin,
  RoleEnum.Moderator,
];
