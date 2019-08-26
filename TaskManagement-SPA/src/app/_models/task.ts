export interface ITask {
    id: number;
    name: string;
    oldName: string;
    description: string;
    startDate: Date;
    endDate: Date;
    userId: number; 
    userName: string;   
}