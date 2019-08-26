import { Component, OnInit, OnDestroy } from "@angular/core";
import { Message } from "primeng/api";
import { ITask } from "src/app/_models/task";
import { TaskService } from "src/app/_services/task.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-task-detail",
  templateUrl: "./task-detail.component.html",
  styleUrls: ["./task-detail.component.css"]
})
export class TaskDetailComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  growlMessage: Message[] = [];
  selectedTaskId: number;
  selectedTask: ITask;
  private sub: any;

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      if (params.id && +params.id) {
        this.selectedTaskId = +params.id;
        this.loadSelectedTask(this.selectedTaskId);
      }
    });
  }

  loadSelectedTask(id: number) {
    this.isLoading = true;
    this.taskService.getTask(id).subscribe(
      (task: ITask) => {
        this.selectedTask = task;
      },
      error => {
        this.isLoading = false;
        this.growlMessage = [];
        this.growlMessage = [];
        this.growlMessage.push({
          severity: "error",
          summary: "Failed to fetch task",
          detail: ""
        });
      },
      () => {
        this.isLoading = false;
      }
    );
  }

  goBack() {
    this.router.navigate(["/task"]);
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
