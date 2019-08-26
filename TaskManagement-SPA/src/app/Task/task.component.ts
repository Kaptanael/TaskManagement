import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Message, ConfirmationService, LazyLoadEvent } from "primeng/api";
import { TaskService } from "src/app/_services/task.service";
import { ITask } from "../_models/task";
import { AuthService } from "../_services/auth.service";

@Component({
  selector: "app-task",
  templateUrl: "./task.component.html",
  styleUrls: ["./task.component.css"],
  providers: [ConfirmationService]
})
export class TaskComponent implements OnInit {
  isLoading: boolean = false;
  growlMessage: Message[] = [];
  tasks: Array<ITask> = [];
  totalTasks: number;
  selectedTask: ITask;
  role: string;

  constructor(
    private router: Router,
    private confirmationService: ConfirmationService,
    private taskService: TaskService,
    private authService: AuthService
  ) {}

  ngOnInit() {    
    //console.log(this.authService.decodedToken);
    //console.log(this.authService.decodedToken.nameid);
    //console.log(this.authService.decodedToken.role);
    if (this.authService.decodedToken.role === "Admin") {      
      this.loadTasks();
    } else {      
      this.loadTasksByUserId(this.authService.decodedToken.nameid);
    }
  }

  isAdmin(){
    if (this.authService.decodedToken.role === "Admin") {      
      return true;
    }
    return false;
  }

  loadTasks() {
    this.isLoading = true;
    this.taskService.getTasks().subscribe(
      (tasks: any[]) => {
        this.tasks = tasks;
        this.totalTasks = this.tasks.length;
      },
      error => {
        this.isLoading = false;
        this.growlMessage = [];
        this.growlMessage = [];
        this.growlMessage.push({
          severity: "error",
          summary: "Failed to fetch tasks",
          detail: ""
        });
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  loadTasksByUserId(id: number) {
    this.isLoading = true;
    this.taskService.getTasksByUserId(id).subscribe(
      (tasks: any[]) => {
        this.tasks = tasks;
        this.totalTasks = this.tasks.length;
      },
      error => {
        this.isLoading = false;
        this.growlMessage = [];
        this.growlMessage = [];
        this.growlMessage.push({
          severity: "error",
          summary: "Failed to fetch tasks",
          detail: ""
        });
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  onCreateTask() {
    this.router.navigate(["/task-add-edit"]);
  }

  onShowTask(task: ITask) {
    if (task) {
      this.selectedTask = task;
      this.router.navigate(["/task-detail", this.selectedTask.id]);
    }
  }

  onUpdateTask(task: ITask) {
    if (task) {
      this.selectedTask = task;
      this.router.navigate(["/task-add-edit", this.selectedTask.id]);
    }
  }

  onDeleteTask(task: ITask) {
    if (task) {
      this.selectedTask = task;
      this.confirmationService.confirm({
        message: "Are you sure that you want to delete this task?",
        header: "Delete Confirmation",
        icon: "fa fa-trash",
        key: "task",
        accept: () => {
          this.isLoading = true;
          this.taskService.deleteTask(this.selectedTask.id).subscribe(
            data => {
              this.growlMessage = [];
              this.growlMessage.push({
                severity: "success",
                summary: "Task has been deleted successfully",
                detail: ""
              });
            },
            error => {
              this.isLoading = false;
              this.growlMessage = [];
              this.growlMessage.push({
                severity: "error",
                summary: "Failed to delete the task",
                detail: ""
              });
            },
            () => {
              this.isLoading = false;
              const index = this.tasks.findIndex(
                x => x.id === this.selectedTask.id
              );
              this.tasks.splice(index, 1);
            }
          );
        }
      });
    }
  }
}
