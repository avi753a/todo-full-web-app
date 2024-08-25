export class PriorityView {
    id: number;
    name?: string;
    description?: string;
    value: number;
    colour?: string;
  
    constructor(id: number, name: string, value: number, description?: string, colour?: string) {
      this.id = id;
      this.name = name;
      this.description = description;
      this.value = value;
      this.colour = colour;
    }
  }