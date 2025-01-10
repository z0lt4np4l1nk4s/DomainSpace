export class DropdownButtonModel {
  text: string;
  onClick: () => void;

  constructor(text: string, onClick: () => void) {
    this.text = text;
    this.onClick = onClick;
  }
}
