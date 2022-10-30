export interface IUser {
    id: string;
    name: string;
}

export interface IGame {
    min: number;
    max: number;
}

export interface IAttempt {
    correctAnswer: boolean;
    attempts: number;
    isGreater: boolean;
}