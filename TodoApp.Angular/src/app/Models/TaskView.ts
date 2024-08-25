export class TaskView {

    constructor(
      public id: string,
      public name: string,
      public description: string,
      public createdTime: Date,
      public editedTime: Date,
      public status: string,
      public priority: string,
      public priorityValue: number
    ) {
  
    }
  }