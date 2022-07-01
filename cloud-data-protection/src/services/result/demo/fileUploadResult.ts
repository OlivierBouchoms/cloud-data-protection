import FileDestination from "entities/fileDestination";

export interface FileUploadResult {
    id: string;
    bytes: number;
    contentType: string;
    displayName: string;
    hasErrors: boolean;
    success: boolean;
    uploadedTo: FileIUploadDestinationResultEntry[];
    scanInfo: FileUploadScanInfoOutput;
}

export interface FileIUploadDestinationResultEntry {
    fileDestination: FileDestination;
    description: string;
    success: boolean;
}

export interface FileUploadScanInfoOutput {
    destination: string;
    widgetUrl: string;
}