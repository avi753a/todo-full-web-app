// public Guid Id { get; set; }
// public string Name { get; set; }
// public string Description { get; set; }
// public DateTime CreatedTime { get; set; }
// public DateTime EditedTime { get; set; }
// public Guid UserId { get; set; }
// public int StatusId { get; set; }
// public int PriorityId { get; set; }


export class TaskDetails{
    constructor(
        public id:string,
        public name:string,
        public description:string,
        public createdTime:Date,
        public editedTime:Date,
        public statusId:number,
        public priorityId:number
    )
    {

    }
}