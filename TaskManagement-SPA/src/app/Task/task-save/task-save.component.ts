import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Message } from 'primeng/api';
import { UserService } from 'src/app/_services/user.service';
import { TaskService } from 'src/app/_services/task.service';
import { ITask } from 'src/app/_models/task';

@Component({
  selector: 'app-task-save',
  templateUrl: './task-save.component.html',
  styleUrls: ['./task-save.component.css']
})
export class TaskSaveComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  growlMessage: Message[] = [];
  taskFormGroup: FormGroup;
  users: Array<any> = [];
  oldTaskName: string;
  selectedTaskId: number;
  selectedTask: ITask;
  private sub: any;
  private isNew = true;

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.createTaskForm();
    this.loadUserNames();
    this.sub = this.route.params.subscribe(params => {
      if (params.id && +params.id) {
        this.selectedTaskId = +params.id;
        this.loadSelectedTask(this.selectedTaskId);
        this.isNew = false;
      } else {
        this.isNew = true;
      }
    });
  }

  loadUserNames() {
    this.isLoading = true;
    this.userService.getUserNames().subscribe((users: any[]) => {
      this.users = users;
    }, error => {
      this.isLoading = false;
      this.growlMessage = [];
      this.growlMessage = []; this.growlMessage.push({ severity: 'error', summary: 'Failed to fetch user names', detail: '' });
    },
    () => {
      this.isLoading = false;
    });
  }

  loadSelectedTask(id: number) {
    this.isLoading = true;
    this.taskService.getTask(id).subscribe((task: ITask) => {
      this.selectedTask = task;
      this.oldTaskName = this.selectedTask.name;
      this.taskFormGroup.controls.name.setValue(this.titleCaseWord(this.selectedTask.name));
      this.taskFormGroup.controls.description.setValue(this.selectedTask.description);
      this.taskFormGroup.controls.startDate.setValue(this.parseDate(this.selectedTask.startDate));
      this.taskFormGroup.controls.endDate.setValue(this.parseDate(this.selectedTask.endDate));
      this.taskFormGroup.controls.userId.setValue(this.selectedTask.userId);
    }, error => {
      this.isLoading = false;
      this.growlMessage = [];
      this.growlMessage = []; this.growlMessage.push({ severity: 'error', summary: 'Failed to fetch task', detail: '' });
    },
    () => {
      this.isLoading = false;
    });
  }

  createTaskForm() {
    this.taskFormGroup = this.fb.group({
      name: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64)]],
      description: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(512)]],
      startDate: ['', [Validators.required]],
      endDate: ['', [Validators.required]],
      userId: ['', Validators.required]
    });
  }

  noWhitespaceValidator(control: AbstractControl) {
    if (control && control.value && !control.value.replace(/\s/g, '').length) {
      control.setValue('');
    }
    return null;
  }

  onStartDateBlur($event) {
    if (!this.isValidDate($event.target.value.toString().trim())) {
      $event.srcElement.value = '';
      $event.target.value = '';
      this.taskFormGroup.patchValue({
        startDate: ''
      });
    }
  }

  onEndDateBlur($event) {
    if (!this.isValidDate($event.target.value.toString().trim())) {
      $event.srcElement.value = '';
      $event.target.value = '';
      this.taskFormGroup.patchValue({
        endDate: ''
      });
    }
  }

  onTaskSave() {
    if (this.isNew && this.taskFormGroup.valid) {
      const taskModel: any = {
        id: 0,
        name: this.taskFormGroup.get('name').value,
        oldName: '',
        description: this.taskFormGroup.get('description').value,
        startDate: this.taskFormGroup.get('startDate').value,
        endDate: this.taskFormGroup.get('endDate').value,
        userId: this.taskFormGroup.get('userId').value
      };
      this.isLoading = true;
      this.taskService.saveTask(taskModel).subscribe(() => {
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'success', summary: 'Task has been added successfully', detail: '' });
      }, error => {
        this.isLoading = false;
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'error', summary: 'Failed to save task', detail: '' });
      },
        () => {
          this.isLoading = false;
          this.onTaskClear();
          this.router.navigate(['/task']);
        });
    }

    if (!this.isNew && this.taskFormGroup.valid) {
      const taskModel: any = {
        id: this.selectedTaskId,
        name: this.taskFormGroup.get('name').value,
        oldName: this.oldTaskName,
        description: this.taskFormGroup.get('description').value,
        startDate: this.taskFormGroup.get('startDate').value,
        endDate: this.taskFormGroup.get('endDate').value,
        userId: this.taskFormGroup.get('userId').value
      };
      this.isLoading = true;
      this.taskService.updateTask(taskModel).subscribe(() => {
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'success', summary: 'Task has been updated successfully', detail: '' });
      }, error => {
        this.isLoading = false;
        this.growlMessage = [];
        this.growlMessage.push({ severity: 'error', summary: 'Failed to update task', detail: '' });
      },
        () => {
          this.isLoading = false;
          this.onTaskClear();
          this.router.navigate(['/task']);
        });
    }
  }

  onNameBlur(name) {
    if (this.isNew && name) {
      this.isLoading = true;
      this.taskService.isExistTaskName(name.trim())
        .subscribe(responeData => {
          if (responeData.body === true) {
            this.taskFormGroup.controls.name.setErrors({ invalid: true });
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Task name is already in use', detail: '' });
          }
        },
          error => {
            this.isLoading = false;  
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Failed to check duplicate task name', detail: '' });
          },() => {
            this.isLoading = false;            
          });
    }

    if (!this.isNew && name && this.oldTaskName && name.trim() !== this.oldTaskName.trim()) {
      this.isLoading = true;
      this.taskService.isExistOldTaskName(this.oldTaskName, name.trim())
        .subscribe(responeData => {
          if (responeData.body === true) {
            this.taskFormGroup.controls.name.setErrors({ invalid: true });
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Task name is already in use', detail: '' });
          }
        },
          error => {
            this.isLoading = false;
            this.growlMessage = [];
            this.growlMessage.push({ severity: 'error', summary: 'Failed to check duplicate task name', detail: '' });
          },() => {
            this.isLoading = false;            
          });
    }
  }

  onTaskClear() {
    this.taskFormGroup.controls.name.setValue('');
    this.taskFormGroup.controls.description.setValue('');
    this.taskFormGroup.controls.startDate.setValue('');
    this.taskFormGroup.controls.endDate.setValue('');
    this.taskFormGroup.controls.userId.setValue('');
    this.oldTaskName = null;
    this.selectedTaskId = null;
    this.selectedTask = null;
  }

  goBack() {
    this.router.navigate(['/task']);
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  titleCaseWord(word: string) {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1).toLowerCase();
  }

  isValidDate(dateString) {
    // First check for the pattern
    if (!/^\d{1,2}\-\d{1,2}\-\d{4}$/.test(dateString)) {
      return false;
    }

    // Parse the date parts to integersKD
    const parts = dateString.split('-');
    const day = parseInt(parts[1], 10);
    const month = parseInt(parts[0], 10);
    const year = parseInt(parts[2], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12) {
      return false;
    }

    const monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 === 0 || (year % 100 !== 0 && year % 4 === 0)) {
      monthLength[1] = 29;
    }

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
  }

  parseDate(value: any): Date | null {
    if ((typeof value === 'string') && (value.indexOf('/') > -1)) {
      const str = value.split('/');

      const year = Number(str[2]);
      const month = Number(str[1]) - 1;
      const date = Number(str[0]);

      return new Date(year, month, date);
    } else if ((typeof value === 'string') && value === '') {
      return new Date();
    }
    const timestamp = typeof value === 'number' ? value : Date.parse(value);
    return isNaN(timestamp) ? null : new Date(timestamp);
  }
}
