import { QuestionBase } from "./question-base";
import { StepCardData } from "./step-card-data";

export class StepData {
    isForm: boolean;
    formData: QuestionBase<any>[];
    cardData: StepCardData;
}