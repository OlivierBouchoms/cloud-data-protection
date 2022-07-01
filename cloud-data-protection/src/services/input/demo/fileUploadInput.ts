import FileDestination from "entities/fileDestination";

interface FileUploadInput {
    destinations: FileDestination[];
    runScan: boolean;
}

export default FileUploadInput;