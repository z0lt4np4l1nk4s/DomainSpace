export class JokeModel {
  category?: string;
  type?: string;
  joke?: string;

  constructor({ category, type, joke }: JokeModel) {
    this.category = category;
    this.type = type;
    this.joke = joke;
  }

  static fromJson(data: any): JokeModel {
    return new JokeModel({
      category: data["category"],
      type: data["type"],
      joke: data["joke"],
    });
  }
}
