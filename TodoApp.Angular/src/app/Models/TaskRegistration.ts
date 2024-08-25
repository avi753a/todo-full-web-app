export class TaskRegistration {
    name: string;
    description: string;
    priorityId: number;
    statusId: number;
  
    constructor(name: string, description: string, priorityId: number, statusId: number) {
      this.name = name;
      this.description = description;
      this.priorityId = priorityId;
      this.statusId = statusId;
    }
  }
  
  