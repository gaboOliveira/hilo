import styled from 'styled-components'

const Wrapper = styled.div`
    display: flex;
    flex-direction: column;
    background-color: #CCC;
    justify-content: center;
    align-items: center;
    height: 100vh;
    gap: 1rem;
    overflow-y: hidden;
`;

const Input = styled.input`
  padding: 1em;
  margin: 1em;
  border: none;
  border-radius: 5px;
`;

const Title = styled.div`
    font-size: 3rem;
    font-weight: bold;
    text-align: center;
`

const RequestNameMessage = styled.span<{size?: 'small'}>`
    font-size: ${props => props.size === 'small' ? '1rem' : "2rem"};
    text-align: center;
`

const StartButton = styled.button`
    text-transform: capitalize;
    font-size: 2rem;
    padding: 1rem 2rem;
    border-radius: 2rem;
    border: none;
    cursor: pointer;

    &:hover {
        background-color: #077bad;
        color: #fff
    }
`

export {
    Wrapper,
    Input,
    Title,
    RequestNameMessage,
    StartButton
}