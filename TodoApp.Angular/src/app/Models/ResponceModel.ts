export class ResponceModel{
    constructor(
        public statusCode:number,
        public isSuccess:boolean,
        public message:string,
        public value:any
    )
    {}
}