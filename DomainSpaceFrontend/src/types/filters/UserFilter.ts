import { BaseFilter } from "./BaseFilter";

export class UserFilter extends BaseFilter {
  roles?: string;

  constructor({ roles, ...other }: UserFilter) {
    super(other);
    this.roles = roles;
  }
}
