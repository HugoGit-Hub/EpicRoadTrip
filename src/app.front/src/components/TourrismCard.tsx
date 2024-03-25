import Card from './Card'

interface ITourismCardProps {
    city: string
}

function TourrismCard({city}: ITourismCardProps) {
  return (
    <Card title={`Tourisme à ${city}`}>

    </Card>
  )
}

export default TourrismCard