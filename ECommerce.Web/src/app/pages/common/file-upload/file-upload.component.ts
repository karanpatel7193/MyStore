import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FileModel } from '../../../models/file.model';
import { ToastService } from '../../../services/toast.service';


@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
    @Input() selectedFile: string = '';
    @Input() id: string = 'fileUpload';
    @Input() name: string = 'fileUpload';
    @Input() pattern: RegExp = /image-*/;
    //@Input() pattern: RegExp = /(image-.*|video-.*)/;

    @Input() required: boolean = false;

    @Input() accept: string = 'image/jpg, image/jpeg, image/png, image/gif';
    @Input() inValidFileMessage: string = 'Only .jpg, .jpeg, .png, .gif Files allowed';
    @Input() extentions: string = 'jpg,jpeg,png,gif';

    @Output() onFileSelected = new EventEmitter<FileModel>();

    public UploadedFiles: FileModel = new FileModel();

    constructor(private myToaster: ToastService) {
    }

    ngOnInit() {
    }

    public isInvalidFile: boolean = false;

    public handleFileUpload(event: Event): void {
        const inputElement = event.target as HTMLInputElement;
        const files = inputElement.files;
        if (files && files.length > 0) {
            const file = files[0];
    
            let fileExtension = file.name.split('.').pop() || '';
            if (!this.extentions.split(',').map(x => x.trim()).includes(fileExtension)){
                this.isInvalidFile = true; 
                this.myToaster.error(this.inValidFileMessage, 'Invalid Format');
                return;
            }
    
            this.isInvalidFile = false; // Reset validation flag
            this.selectedFile = files.length > 1 ? `${files.length} files selected.` : files[0].name;
    
            const reader = new FileReader();
            reader.readAsDataURL(file);
    
            reader.onloadend = () => {
                let uploadedFile: FileModel = new FileModel();
                uploadedFile.content = reader.result as string;
                uploadedFile.name = file.name;
                uploadedFile.size = file.size;
                uploadedFile.extention = file.type;
                this.UploadedFiles = uploadedFile;
    
                this.onFileSelected.emit(this.UploadedFiles);
            };
        }
    }
    
    // public handleFileUpload(files: FileList) {
    //     let fileCount: number = files.length;

    //     if (files != null && fileCount > 0) {
    //         let file = files[0];

    //         if (!file.type.match(this.pattern)) {
    //             this.myToaster.error('Provided file format is invalid. Only images are supported.', 'Invalid Format');
    //             return;
    //         }
    //         this.selectedFile = fileCount > 1 ? fileCount + ' files selected.' : files[0].name;

    //         var reader = new FileReader();
    //         reader.readAsDataURL(file);

    //         reader.onloadend = () => {

    //             let UploadedFile: FileModel = new FileModel();
    //             UploadedFile.content = reader.result as string;
    //             UploadedFile.name = file.name;
    //             UploadedFile.size = file.size;
    //             UploadedFile.extention = file.type;
    //             this.UploadedFiles = UploadedFile;

    //             //return UploadedFile;
    //             this.onFileSelected.emit(this.UploadedFiles);
    //         };
    //     }
    // }

}

