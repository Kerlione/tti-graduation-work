import { QuestionBase } from '../models/question-base';

export class DropdownField extends QuestionBase<number> {
    controlType = 'dropdown';
    options: {
        key: string,
        value: number
    }[] = [];
    constructor(options: {} = {}) {
        super(options);
        this.options = options['options'] || [];
    }
}
