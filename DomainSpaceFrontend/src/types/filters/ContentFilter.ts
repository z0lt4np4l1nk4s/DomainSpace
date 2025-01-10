import { BaseFilter } from "./BaseFilter";

export class ContentFilter extends BaseFilter {
  subjectIds?: string;

  constructor({ subjectIds, ...other }: ContentFilter) {
    super(other);
    this.subjectIds = subjectIds;
  }
}
