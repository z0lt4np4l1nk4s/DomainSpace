export class DropdownItemModel {
  value: string;
  label: string;
  data?: any;

  constructor(value: string, label: string, data: any = undefined) {
    this.value = value;
    this.label = label;
    this.data = data;
  }
}
